using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeShopee.Models;

[Table("Cart")]
public class Cart
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [ForeignKey("CustomerId")]
    public Users Customer { get; set; }
    
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    
    public long Quantity { get; set; }
}