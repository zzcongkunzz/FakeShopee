using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeShopee.Models;

[Table("UserGroup")]
public class UserGroup
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column(TypeName = "nvarchar(max)")]
    public string Name { get; set; }
}