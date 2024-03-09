using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FakeShopee.constant;
using Microsoft.AspNetCore.Mvc;

namespace FakeShopee.Models;

[Table("Users")]
public class Users
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserGroupId {get; set;}
    
    [ForeignKey("UserGroupId")]
    public UserGroup UserGroup { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Required(ErrorMessage = "Vui lòng nhập Họ và tên")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có độ dài từ 8 đến 30 kí tự")]
    public string Password { get; set; }

    // [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không hợp lệ")]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không hợp lệ")]
    [Required(ErrorMessage = "Vui lòng nhập Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Số điện thoại")]
    // [RegularExpression(@"^\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string PhoneNumber { get; set; }


    public GenderType? Gender { get; set; }

    [Column(TypeName = "nvarchar(1000)")]
    public string? Address { get; set; }

}