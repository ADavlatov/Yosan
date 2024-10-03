using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Yosan.Gateway.Services;

namespace Yosan.Gateway.RabbitMq;

public class RabbitMqListener : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqListener(IConfiguration config)
    {
        //@TODO вынести подключение в asppsettings
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            Console.WriteLine($"Запрос: {content}");

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