using System.ComponentModel.DataAnnotations.Schema;

namespace a;

public class AuditEntity
{
    [Column("id")]
    public Guid Id {get; set;}
}