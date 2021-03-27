using System.Collections.Generic;
using EventStore.Client;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Infrastructure;

namespace PaymentSystems.WebAPI.Features
{
    public class IntegrationReactorSubscription : SubscriptionService
    {
        public IntegrationReactorSubscription(EventStoreClient eventStoreClient, ICheckpointStore checkpointStore, string subscriptionName, IEnumerable<IEventHandler> eventHandlers) : base(eventStoreClient, checkpointStore, subscriptionName, eventHandlers)
        {
        }
    }
}