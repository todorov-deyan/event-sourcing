using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EventSourcing.Api.Common;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository.Entities
{
    [Table("streams", Schema = Constants.CustomEventSourcingDbScheme)]
    public class CustomStream : BaseEntity
    {
        public CustomStream()
        {
            Events = new HashSet<CustomEvent>();
        }

        [Key]
        [Column("stream_id")]
        public Guid StreamId { get; set; }

        [Column("type")]
        public string Type { get; set; }

        public virtual ICollection<CustomEvent> Events { get; set; }
    }
}
