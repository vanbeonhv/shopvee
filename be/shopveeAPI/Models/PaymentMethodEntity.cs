using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shopveeAPI.Enum;

namespace shopveeAPI.Models;

[Table("payment_method")]
public class PaymentMethodEntity: AuditEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid PaymentTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Provider { get; set; }

    [Required]
    [MaxLength(255)]
    public string AccountNumber { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [ForeignKey("PaymentTypeId")]
    public PaymentType PaymentType { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}