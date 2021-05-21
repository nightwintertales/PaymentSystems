using System;
using System.Threading;
using System.Threading.Tasks;
using Eventuous.Projections.MongoDB.Tools;
using Eventuous.Subscriptions;
using MongoDB.Driver;
using PaymentSystems.WebAPI.Features.Payments;
using static PaymentSystems.Domain.Events.PaymentEvents;

namespace PaymentSystems.WebAPI.Integration.Payments {
    class PaymentsIntegrationEventHandler : IEventHandler {
        readonly IntegrationProducer _producer;
        readonly IMongoDatabase _database;

        public PaymentsIntegrationEventHandler(IntegrationProducer producer, IMongoDatabase database) {
            _producer = producer;
            _database = database;
        }

        //Publish to the queue
        public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken) {
            return evt switch {
                V1.PaymentApproved approved => HandlePaymentApproved(approved),
                // V1.PaymentExecuted executed => null,
                _ => Task.CompletedTask
            };

            async Task HandlePaymentApproved(V1.PaymentApproved paymentApproved) {
                var payment = await _database.LoadDocument<PaymentDocument>(paymentApproved.PaymentId, cancellationToken);

                if (payment == null)
                    throw new InvalidOperationException("This should not happen");

                var approved = new IntegrationPaymentEvents.V1.PaymentApproved(
                    payment.AccountId,
                    paymentApproved.PaymentId,
                    payment.Amount,
                    new IntegrationPaymentEvents.V1.Payee(payment.Payee.Name, payment.Payee.SortCode, payment.Payee.AccountNumber),
                    paymentApproved.ApprovedAt,
                    paymentApproved.ApprovedBy
                );
                await _producer.PublishIntegrationEvent(approved, cancellationToken);
            }
        }

        public string SubscriptionId => IntegrationReactorSubscription.SubscriptionName;
    }
}