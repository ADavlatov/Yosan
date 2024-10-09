using System.Text;
using Google.Protobuf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Yosan.Gateway.Helpers;
using Yosan.Gateway.Services;

namespace Yosan.Gateway.RabbitMq;

public class RabbitMqListener : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private RouterService _router;

    public RabbitMqListener(IConfiguration config)
    {
        //@TODO вынести подключение в asppsettings
        _router = new RouterService();
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var path = CheckRequestPath.Check(content);
            IBufferMessage? message = null;
            if (path != "")
            {
               message = await _router.Execute(path, content);
            }

            if (message != null)
            {
                var props = _channel.CreateBasicProperties();
                props.CorrelationId = ea.BasicProperties.CorrelationId;

                _channel.BasicPublish(exchange: "", routingKey: ea.BasicProperties.ReplyTo, basicProperties: props,
                    body: Encoding.UTF8.GetBytes(message.ToString()));
            }
            
            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("MyQueue", false, consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}