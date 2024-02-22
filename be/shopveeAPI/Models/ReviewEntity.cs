using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

[Table("review")]
public class ReviewEntity: AuditEntity
{
    
    [MaxLength(255)]
    public string Description { get; set; }

    public int? Rating { get; set; }

    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}