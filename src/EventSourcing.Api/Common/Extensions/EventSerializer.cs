using EventSourcing.Api.Common.EventSourcing;
using Newtonsoft.Json;

namespace EventSourcing.Api.Common.Extensions
{
    public class EventSerializer : IEventSerializer
    {
        public static T FromJSON<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public virtual string ToJSON(IEventState @event)
        {
            return JsonConvert.SerializeObject(@event);
        }
    }
}
