using EventSourcing.Api.Common.EventSourcing;
using System.Reflection;

namespace EventSourcing.Api.Common.Extensions
{
    public static class EventSerialzierExtension
    {
        public static IServiceCollection AddEventJsonSerializer(this IServiceCollection services)
        {
            return services.AddScoped<IEventSerializer>(x =>
                            {
                                var serializer = new JsonEventSerializer();
                                serializer.ScanEvents(Assembly.GetExecutingAssembly());
                                return serializer;
                            });
        }
    }
}
