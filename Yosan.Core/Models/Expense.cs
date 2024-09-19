using Yosan.Core.Interfaces;

namespace Yosan.Core.Models;

public class Expense : IElement
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public int Sum { get; set; }
    public DateTime Date { get; set; }
}