using System.ComponentModel.DataAnnotations.Schema;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository.Entities
{
    public abstract class BaseEntity
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
