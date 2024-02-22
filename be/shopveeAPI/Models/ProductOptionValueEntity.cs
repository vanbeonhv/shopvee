using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("product_option_value")]
public class ProductOptionValueEntity: AuditEntity
{
    [Required]
    [ForeignKey("ProductOption")]
    public Guid OptionId { get; set; }

    [Required]
    public string Value { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }

    [Required]
    public decimal BasePrice { get; set; }

    [Required]
    public decimal SalePrice { get; set; }

    [Required]
    public decimal SalePercentage { get; set; }

    public int? SoldQuantity { get; set; }

    public int? AvailableQuantity { get; set; }

    [ForeignKey("OptionId")]
    public ProductOptionEntity ProductOption { get; set; }

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; }
}