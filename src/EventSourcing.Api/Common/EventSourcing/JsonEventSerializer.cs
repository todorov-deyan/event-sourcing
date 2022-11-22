﻿using EventSourcing.Api.Common.CQRS;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EventSourcing.Api.Common.EventSourcing
{
    public class JsonEventSerializer : IEventSerializer
    {
        private Dictionary<string, Type> _eventTypes = new();

        public JsonEventSerializer()
        {

        }

        public void ScanEvents(Assembly assembly)
        {
            var events = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IEventState)));

            foreach (var @event in events)
            {
                _eventTypes.Add(@event.Name.ToLowerInvariant(), @event);
            }
        }

        public IEventState? DeserializeEvent(string eventType, string jsonData)
        {
            if (eventType == null) throw new ArgumentNullException(nameof(eventType));
            if (jsonData == null) throw new ArgumentNullException(nameof(jsonData));


            Type eType = _eventTypes.ContainsKey(eventType) ? _eventTypes[eventType] : null;
            if (eType is null) throw new InvalidOleVariantTypeException(nameof(eType));


            return (IEventState?)this.GetType()
                                     .GetMethod("Deserialize")
                                     .MakeGenericMethod(eType)
                                     .Invoke(this, new object[] { jsonData });
        }


        public T? Deserialize<T>(string jsonData) where T : IEventState
        {
            if (jsonData == null) throw new ArgumentNullException(nameof(jsonData));

            //return JsonSerializer.Deserialize<T>(jsonData);

            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public string Serialize<T>(T @event) where T : IEventState
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            //return JsonSerializer.Serialize(@event);

            return JsonConvert.SerializeObject(@event);
        }

        
    }
}
