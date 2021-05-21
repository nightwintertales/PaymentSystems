using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using Eventuous;
using Eventuous.Producers.EventStoreDB;

namespace PaymentSystems.WebAPI.Integration {
    public class IntegrationProducer : EventStoreProducer {
        public IntegrationProducer(EventStoreClient eventStoreClient, IEventSerializer serializer)
            : base(eventStoreClient, serializer) { }

        public Task PublishIntegrationEvent<T>(T evt, CancellationToken cancellationToken) where T : class
            => Produce("integration", evt, cancellationToken);
    }
}