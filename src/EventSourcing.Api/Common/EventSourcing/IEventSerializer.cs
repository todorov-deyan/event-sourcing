namespace EventSourcing.Api.Common.EventSourcing
{
    public interface IEventSerializer
    {
        string Serialize<T>(T @event) where T : IEventState;
        T Deserialize<T>(string data) where T : IEventState;
        IEventState DeserializeEvent(string eventType, string jsonData);

    }
}
