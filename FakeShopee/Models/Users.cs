using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FakeShopee.constant;

namespace FakeShopee.Models;

[Table("Users")]
public class Users
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [ForeignKey("UserGroupId")]
    public UserGroup UserGroup { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string FullName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public GenderType? Gender { get; set; }

    [Column(TypeName = "nvarchar(1000)")]
    public string? Address { get; set; }
}