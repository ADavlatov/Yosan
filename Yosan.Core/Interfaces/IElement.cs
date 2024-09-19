namespace Yosan.Core.Interfaces;

public interface IElement
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Sum { get; set; }
    public DateTime Date { get; set; }
}