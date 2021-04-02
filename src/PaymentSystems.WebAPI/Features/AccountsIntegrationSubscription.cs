using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using MassTransit;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Application;
using PaymentSystems.WebAPI.Infrastructure;
using static PaymentSystems.Domain.Accounts.AccountEvents;


namespace PaymentSystems.WebAPI.Features
{
    public class AccountsIntegrationSubscription : SubscriptionService
    {
        const string SubscriptionName = "AccountsIntegration";

        public AccountsIntegrationSubscription(
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
            readonly AccountCommandService _commandService;

            public string SubscriptionGroup => SubscriptionName;

            public TransactionEventHandler(IPublishEndpoint publisher) => _publisher = publisher;

            public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken)
            {
                return evt switch
                {
                    V1.AccountOpened accountOpened =>
                        _publisher.Publish(accountOpened),
                    V1.TransactionInitiated transactionInitiated =>
                        _publisher.Publish(transactionInitiated),
                    V1.TransactionBooked transactionBooked =>
                         _publisher.Publish(transactionBooked),

                    _ => Task.CompletedTask
                };
            }
        }
    }
}