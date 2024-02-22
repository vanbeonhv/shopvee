using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

public class AuditEntity
{
    [Key]
    [Column("id")]
    public Guid Id {get; set;}
    [Column("created_at")]
    public DateTime? CreatedAt {get; set;} = DateTime.UtcNow;
    [Column("created_by")]
    public Guid? CreatedBy {get; set;}
    [Column("updated_at")]
    public DateTime? UpdatedAt {get; set;}
    [Column("updated_by")]
    public Guid? UpdatedBy {get; set;}
}