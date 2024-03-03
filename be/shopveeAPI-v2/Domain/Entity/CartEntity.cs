using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

[Table("cart")]
public class CartEntity: AuditEntity
{
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Shop")]
    public Guid ShopId { get; set; }
    
    // Navigation properties for related entities
    [ForeignKey("ShopId")]
    public ShopEntity Shop { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}