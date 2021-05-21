using System.Collections.Generic;
using EventStore.Client;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.EventStoreDB;

namespace PaymentSystems.WebAPI.Integration {
    public class IntegrationReactorSubscription : AllStreamSubscription {
        public const string SubscriptionName = "IntegrationsReactor";

        public IntegrationReactorSubscription(
            EventStoreClient           eventStoreClient,
            ICheckpointStore           checkpointStore,
            IEnumerable<IEventHandler> eventHandlers
        ) : base(
            eventStoreClient,
            SubscriptionName,
            checkpointStore,
            eventHandlers
        ) { }
    }
}