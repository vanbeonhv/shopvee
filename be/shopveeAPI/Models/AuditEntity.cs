using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class AuditEntity
{
    [Column("id")]
    public Guid Id {get; set;}
}