using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Application;
using PaymentSystems.WebAPI.Infrastructure;
using static PaymentSystems.Contract.AccountCommands;
using static PaymentSystems.Domain.Accounts.AccountEvents;

namespace PaymentSystems.WebAPI.Features {
    public class AccountReactorSubscription : SubscriptionService {
        const string SubscriptionName = "AccountReactors";

        public AccountReactorSubscription(
            EventStoreClient      eventStoreClient,
            ICheckpointStore      checkpointStore,
            AccountCommandService commandService
        ) : base(
            eventStoreClient,
            checkpointStore,
            SubscriptionName,
            new[] {new TransactionEventHandler(commandService)}
        ) { }

        class TransactionEventHandler : IEventHandler {
            readonly AccountCommandService _commandService;

            public string SubscriptionGroup => SubscriptionName;

            public TransactionEventHandler(AccountCommandService commandService) {
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
                                initiated.AvailableBalance,
                                initiated.InitiatedAt),
                            cancellationToken
                        ),
                    V1.TransactionBooked booked =>
                        _commandService.HandleExisting(
                            new BookTransaction(
                                booked.AccountId,
                                booked.TransactionId, 
                                booked.BookedAt),
                            cancellationToken
                        ),
                    V1.TransactionCancelled denied =>
                        _commandService.HandleExisting(
                            new CancelTransaction(
                                denied.AccountId,
                                denied.TransactionId,
                                denied.Reason,
                                denied.CancelledAt
                            ),
                            cancellationToken
                        ),
                    _ => Task.CompletedTask
                };
            }
        }
    }
}