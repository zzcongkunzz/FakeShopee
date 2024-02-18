using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeShopee.Models;

[Table("Category")]
public class Category
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; }
}