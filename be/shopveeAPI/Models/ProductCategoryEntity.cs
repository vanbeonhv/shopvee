using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("product_category")]
public class ProductCategoryEntity: AuditEntity
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    [MaxLength(255)]
    public string Description { get; set; }
    public ICollection<ProductEntity> Products { get; set; }
}