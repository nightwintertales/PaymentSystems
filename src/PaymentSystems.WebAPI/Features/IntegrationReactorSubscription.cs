using System.Collections.Generic;
using EventStore.Client;
using System.Threading;
using System.Threading.Tasks;
using Eventuous.Subscriptions;
using PaymentSystems.Domain.Events;
using SubscriptionService = PaymentSystems.WebAPI.Infrastructure.SubscriptionService;

namespace PaymentSystems.WebAPI.Features
{
    public class IntegrationReactorSubscription : SubscriptionService
    {
        const string SubscriptionName = "IntegrationsReactor";

        public IntegrationReactorSubscription(EventStoreClient eventStoreClient, ICheckpointStore checkpointStore,
            string subscriptionName, IEnumerable<IEventHandler> eventHandlers) : base(eventStoreClient,
            checkpointStore, SubscriptionName, eventHandlers)
        {
            
        }
            
            public IntegrationReactorSubscription(
            EventStoreClient      eventStoreClient,
            ICheckpointStore      checkpointStore
        ) : base(
            eventStoreClient,
            checkpointStore,
            SubscriptionName,
            new[] {new TransactionEventHandler()}
        ) { }

        class TransactionEventHandler : IEventHandler {
            

            public string SubscriptionGroup => SubscriptionName;


            //Publish to the queue
            public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken) {
                return evt switch {
                    PaymentEvents.V1.PaymentApproved approved =>
                    null,
                    PaymentEvents.V1.PaymentExecuted executed => 
                        null,
                    _ => Task.CompletedTask
                };
            }

            public string SubscriptionId { get; }
        }
    }
}


