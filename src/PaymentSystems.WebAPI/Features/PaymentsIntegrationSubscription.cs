

/*using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using Eventuous;
using Eventuous.Subscriptions;
using PaymentSystems.Domain.Payments;
using PaymentSystems.WebAPI.Integration.Payments;
using static PaymentSystems.Domain.Events.PaymentEvents;

namespace PaymentSystems.WebAPI.Features {
    public class PaymentsIntegrationSubscription : SubscriptionService {
        const string SubscriptionName = "PaymentsIntegration";

        public PaymentsIntegrationSubscription(
            EventStoreClient eventStoreClient,
            ICheckpointStore checkpointStore,
            IPublishEndpoint publisher,
            IAggregateStore store
        ) : base(
            eventStoreClient,
            checkpointStore,
            SubscriptionName,
            new[] {new TransactionEventHandler(x => publisher.Publish(x), store)}
            
            // Handle also PaymentExecuted and remove it from the pending payments list
        ) { }

        class TransactionEventHandler : IEventHandler {
            readonly Func<object, Task> _publishEvent;
            readonly IAggregateStore    _store;

            public string SubscriptionGroup => SubscriptionName;

            public TransactionEventHandler(Func<object, Task> publishEvent, IAggregateStore store) {
                _publishEvent = publishEvent;
                _store        = store;
            }

            public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken) {
                return evt switch {
                    V1.PaymentApproved paymentApproved => HandlePaymentApproved(paymentApproved),
                    _                                  => Task.CompletedTask
                };

                async Task HandlePaymentApproved(V1.PaymentApproved paymentApproved) {
                    var payment = await _store.Load<Payment, PaymentId, PaymentState>(
                        new PaymentId(paymentApproved.PaymentId),
                        cancellationToken
                    );

                    var approved = new IntegrationPaymentEvents.V1.PaymentApproved(
                        payment.State.AccountId,
                        paymentApproved.PaymentId,
                        payment.State.Amount,
                        paymentApproved.ApprovedAt,
                        paymentApproved.ApprovedBy
                    );
                    await _publishEvent(approved);
                }
            }
        }

        protected override Task<EventSubscription> Subscribe(Checkpoint checkpoint, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<EventPosition> GetLastEventPosition(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
*/