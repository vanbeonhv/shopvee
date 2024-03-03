using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

[Table("cart_item")]
public class CartItemEntity: AuditEntity
{
    [Required]
    [ForeignKey("Cart")]
    public Guid CartId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }
    
    // Navigation properties for related entities
    [ForeignKey("CartId")]
    public CartEntity Cart { get; set; }

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; }
}