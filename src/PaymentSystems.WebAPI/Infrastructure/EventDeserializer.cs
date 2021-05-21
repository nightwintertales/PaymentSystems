using System.Text.Json;
using EventStore.Client;
using Eventuous;

namespace PaymentSystems.WebAPI.Infrastructure {
    public static class EventDeserializer {
        public static object Deserialize(this ResolvedEvent resolvedEvent) {
            var dataType = TypeMap.GetType(resolvedEvent.Event.EventType);
            return dataType == null ? null : JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, dataType);
        }
    }
}
