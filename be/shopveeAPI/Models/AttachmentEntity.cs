using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("attachment")]
public class AttachmentEntity: AuditEntity
{
    [MaxLength(255)]
    public string Url { get; set; }

    public int? Type { get; set; }

    [Required]
    [ForeignKey("Review")]
    public Guid ReviewId { get; set; }

    // Navigation property for related Review
    [ForeignKey("ReviewId")]
    public ReviewEntity Review { get; set; }
}