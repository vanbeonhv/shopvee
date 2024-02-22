using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shopveeAPI.Enum;

namespace shopveeAPI.Models;

[Table("address")]
public class AddressEntity: AuditEntity
{
    [Required]
    public string City { get; set; }

    [Required]
    [MaxLength(255)]
    public string PostalCode { get; set; }

    [Required]
    [MaxLength(255)]
    public string AddressLine { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public AddressType AddressType { get; set; } = AddressType.Default;
    [ForeignKey("UserId")]
    public User User { get; set; }
}