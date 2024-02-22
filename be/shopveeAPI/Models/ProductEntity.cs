using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("product")]
public class ProductEntity: AuditEntity
{
    [Required]
    public int Name { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    [Required]
    [ForeignKey("ProductCategory")]
    public Guid CategoryId { get; set; }

    [MaxLength(255)]
    public string Image { get; set; }

    [Required]
    [ForeignKey("Shop")]
    public Guid ShopId { get; set; }

    [Required]
    public int SoldQuantity { get; set; } = 0;
    
    [ForeignKey("CategoryId")]
    public ProductCategoryEntity ProductCategory { get; set; }

    [ForeignKey("ShopId")]
    public ShopEntity Shop { get; set; }
}