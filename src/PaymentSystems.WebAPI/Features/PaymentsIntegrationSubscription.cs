using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using MassTransit;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Infrastructure;
using PaymentSystems.Domain.Events;


namespace PaymentSystems.WebAPI.Features
{
    public class PaymentsIntegrationSubscription : SubscriptionService
    {
        const string SubscriptionName = "PaymentsIntegration";

        public PaymentsIntegrationSubscription(
            EventStoreClient eventStoreClient,
            ICheckpointStore checkpointStore,
            IPublishEndpoint publisher
        ) : base(
            eventStoreClient,
            checkpointStore,
            SubscriptionName,
            new[] { new TransactionEventHandler(publisher) }
        )
        { }

        class TransactionEventHandler : IEventHandler
        {
            readonly IPublishEndpoint _publisher;

            public string SubscriptionGroup => SubscriptionName;

            public TransactionEventHandler(IPublishEndpoint publisher) => _publisher = publisher;

            public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken)
            {
                return evt switch
                {
                    PaymentEvents.V1.PaymentApproved paymentApproved =>
                        _publisher.Publish(paymentApproved),
                    _ => Task.CompletedTask
                };
            }
        }
    }
}
