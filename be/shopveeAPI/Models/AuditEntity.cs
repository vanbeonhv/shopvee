using System.ComponentModel.DataAnnotations.Schema;

namespace shopveeAPI.Models;

public class AuditEntity
{
    [Column("id")]
    public Guid Id {get; set;}
}