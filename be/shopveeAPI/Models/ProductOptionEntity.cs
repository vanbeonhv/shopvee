using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("product_option")]
public class ProductOptionEntity: AuditEntity
{
    [Required]
    [MaxLength(255)]
    public string OptionName { get; set; }
    public ICollection<ProductOptionValueEntity> ProductOptionValues { get; set; }
}