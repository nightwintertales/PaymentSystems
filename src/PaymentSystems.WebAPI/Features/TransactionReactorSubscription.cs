using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.EventStoreDB;
using PaymentSystems.WebAPI.Application;
using static PaymentSystems.Contract.AccountCommands;
using static PaymentSystems.Domain.Transactions.TransactionEvents;

namespace PaymentSystems.WebAPI.Features
{
    public class  TransactionReactorSubscription : AllStreamSubscription {
        const string SubscriptionName = "TransactionReactors";

        public TransactionReactorSubscription(
            EventStoreClient      eventStoreClient,
            ICheckpointStore      checkpointStore,
            TransactionsCommandService commandService
        ) : base(
            eventStoreClient,
            SubscriptionName,
            checkpointStore,
            new[] {new TransactionEventHandler(commandService)}
        ) { }

        class TransactionEventHandler : IEventHandler {
            readonly TransactionsCommandService _commandService;

            public string SubscriptionGroup => SubscriptionName;

            public TransactionEventHandler(TransactionsCommandService commandService) {
                _commandService = commandService;
            }

            public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken) {
                return evt switch {
                    V1.TransactionInitiated initiated =>
                        _commandService.HandleExisting(
                            new InitiateTransaction(
                                initiated.AccountId,
                                initiated.TransactionId,
                                initiated.Amount,
                                initiated.InitiatedAt
                            ),
                            cancellationToken
                        ),
                    V1.TransactionBooked booked =>
                        _commandService.HandleExisting(
                            new BookTransaction(
                                booked.AccountId,
                                booked.TransactionId,
                                booked.BookedAt
                            ),
                            cancellationToken
                        ),
                    V1.TransactionDenied denied =>
                        _commandService.HandleExisting(
                            new CancelTransaction(
                                denied.AccountId,
                                denied.TransactionId,
                                denied.Reason,
                                denied.DeniedAt
                            ),
                            cancellationToken
                        ),
                    _ => Task.CompletedTask
                };
            }

            public string SubscriptionId { get; }
        }
    }
}