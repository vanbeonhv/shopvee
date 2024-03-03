using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

[Table("product_option_value")]
public class ProductOptionValueEntity : AuditEntity
{
    [Required]
    [ForeignKey("ProductOption")]
    [Column("option_id")]
    public Guid OptionId { get; set; }

    [Required]
    [Column("value")]
    public string Value { get; set; } = null!;

    [Required]
    [ForeignKey("Product")]
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Required]
    [Column("base_price")]
    public decimal BasePrice { get; set; }

    [Required]
    [Column("sale_price")]
    public decimal SalePrice { get; set; }

    [Required]
    [Column("sale_percentage")]
    public decimal SalePercentage { get; set; }

    [Column("sold_quantity")]
    public int? SoldQuantity { get; set; }

    [Column("available_quantity")]
    public int? AvailableQuantity { get; set; }

    [ForeignKey("OptionId")]
    public ProductOptionEntity ProductOption { get; set; } = null!;

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; } = null!;
}
