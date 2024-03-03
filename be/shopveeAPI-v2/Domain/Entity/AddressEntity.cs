using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;

namespace Domain.Entity;

[Table("address")]
public class AddressEntity: AuditEntity
{
    [Required]
    [Column("city")]
    public string City { get; set; }

    [Required]
    [Column("postal_code")]
    [MaxLength(255)]
    public string PostalCode { get; set; }

    [Required]
    [Column("address_line")]
    [MaxLength(255)]
    public string AddressLine { get; set; }

    [Required]
    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [Column("address_type")]
    public AddressType AddressType { get; set; } = AddressType.Default;
    [ForeignKey("UserId")]
    public User User { get; set; }
}