using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeShopee.Models;

[Table("OrderDetail")]
public class OrderDetail
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Key, Column(Order = 0), ForeignKey("OrderId")]
    public virtual Order Order { get; set; }

    [Key, Column(Order = 1), ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    public int Quantity { get; set; }

    public long Price { get; set; }

    [Range(0, 100)]
    public int Discount { get; set; }

    //Thành tiền
    public long IntoMoney { get; set; }
}