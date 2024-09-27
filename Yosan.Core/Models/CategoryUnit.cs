namespace Yosan.Core.Models;

public class CategoryUnit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Sum { get; set; }
    public DateOnly Date { get; set; }
    public string UserId { get; set; }
    public Category Category { get; set; }
}