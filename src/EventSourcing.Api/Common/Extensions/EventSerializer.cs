using EventSourcing.Api.Common.EventSourcing;
using Newtonsoft.Json;

namespace EventSourcing.Api.Common.Extensions
{
    public class EventSerializer : IEventSerializer
    {
        public string ToJSON(IEventState @event)
        {
            return JsonConvert.SerializeObject(@event);
        }

        T IEventSerializer.FromJSON<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
