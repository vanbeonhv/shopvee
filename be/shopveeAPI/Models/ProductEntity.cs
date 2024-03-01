using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("product")]
public class ProductEntity: AuditEntity
{
    [Required]
    [Column("name")]
    public string Name { get; set; } = null!;

    [MaxLength(255)]
    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [ForeignKey("ProductCategory")]
    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [MaxLength(255)] 
    [Column("image")]
    public string Image { get; set; } = null!;

    [Required]
    [ForeignKey("Shop")]
    [Column("shop_id")]
    public Guid ShopId { get; set; }

    [Required]
    [Column("sold_quantity")]
    public int SoldQuantity { get; set; } 
    
    [ForeignKey("CategoryId")]
    public ProductCategoryEntity ProductCategory { get; set; } = null!;

    [ForeignKey("ShopId")]
    public ShopEntity Shop { get; set; } = null!;
    public ICollection<ProductOptionValueEntity> ProductOptionValues { get; set; } = null!;
   
}