using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FakeShopee.constant;
using Microsoft.EntityFrameworkCore;

namespace FakeShopee.Models;

[Table("Order")]
public class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [ForeignKey("CustomerId")]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Users Customer { get; set; }

    [ForeignKey("StaffId")]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Users Staff { get; set; }

    public OderType Status { get; set; }

    //Ngày đặt hàng
    public DateTime OrderDate { get; set; }
    
    //Ngày nhận hàng
    public DateTime ReceivedDate { get; set; }

    //Tổng tiền
    public long TotalPrice { get; set; }
}