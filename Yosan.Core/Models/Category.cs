using Yosan.Core.Enums;

namespace Yosan.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CategoryTypes Type { get; set; }
    public string UserId { get; set; }
    public List<CategoryUnit> Units { get; set; }
}