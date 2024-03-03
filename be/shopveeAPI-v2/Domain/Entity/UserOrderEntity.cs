using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

[Table("user_order")]
public class UserOrderEntity: AuditEntity
{
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Cart")]
    public Guid CartId { get; set; }

    [Required]
    public int OrderStatus { get; set; }

    [Required]
    public int PaymentStatus { get; set; }

    [Required]
    [MaxLength(255)]
    public string ShippingAddress { get; set; }

    [Required]
    [ForeignKey("PaymentMethod")]
    public Guid PaymentMethodId { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [ForeignKey("Address")]
    public Guid AddressId { get; set; }
    
    // Navigation properties for related entities
    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("CartId")]
    public CartEntity Cart { get; set; }

    [ForeignKey("PaymentMethodId")]
    public PaymentMethodEntity PaymentMethod { get; set; }

    [ForeignKey("AddressId")]
    public AddressEntity Address { get; set; }
}