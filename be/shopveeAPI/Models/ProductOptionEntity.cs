using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("product_option")]
public class ProductOptionEntity : AuditEntity
{
    [Required] 
    [MaxLength(255)] 
    [Column("option_name")]
    public string OptionName { get; set; } = null!;
    public ICollection<ProductOptionValueEntity> ProductOptionValues { get; set; }
}