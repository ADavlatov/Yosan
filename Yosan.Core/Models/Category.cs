namespace Yosan.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string UserId { get; set; }
    public List<CategoryUnit> Units { get; set; }
}