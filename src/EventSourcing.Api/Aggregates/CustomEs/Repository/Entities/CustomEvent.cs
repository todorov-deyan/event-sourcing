using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EventSourcing.Api.Common;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository.Entities
{
    [Table("events", Schema = Constants.CustomEventSourcingDbScheme)]
    public class CustomEvent : BaseEntity
    {
        [Key]
        [Column("event_id")]
        public Guid EventId { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }
       
        public virtual CustomStream Stream { get; set; }

        [Column("data", TypeName = "jsonb")]
        public string Data { get; set; }

        [Column("event_type")]
        public string EventType { get; set; }
    }
}
