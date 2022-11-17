using EventSourcing.Api.Common.EventSourcing;
using Newtonsoft.Json;

namespace EventSourcing.Api.Common.Extensions
{
    public interface IEventSerializer
    {
        string ToJSON(IEventState @event);
        T FromJSON<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
