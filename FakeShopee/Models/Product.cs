using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeShopee.Models;

[Table("Product")]
public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Column(TypeName = "nvarchar(1000)")]
    public string Name { get; set; }

    public string LinkImage { get; set; }

    public double Rating { get; set; }

    public bool Favorite { get; set; }

    public long Price { get; set; }

    [Range(0, 100)]
    public int Discount { get; set; }

    // Số lượng nhập kho
    public int InventoryQuantity { get; set; }

    // Số lượng tồn kho
    public int WarehouseQuantity { get; set; }

    // Số lượng đã bán
    public int SoldQuantity { get; set; }

        [Column(TypeName = "datetime2")]
    public DateTime ProductImportDate { get; set; }
}