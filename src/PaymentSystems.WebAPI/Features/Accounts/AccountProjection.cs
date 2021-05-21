using System.Collections.Generic;
using System.Threading.Tasks;
using Eventuous.Projections.MongoDB;
using Eventuous.Projections.MongoDB.Tools;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using static MongoDB.Driver.Builders<PaymentSystems.WebAPI.Features.Accounts.AccountDocument>;
using static PaymentSystems.Domain.Accounts.AccountEvents;

namespace PaymentSystems.WebAPI.Features.Accounts {
    public class AccountProjection : MongoProjection<AccountDocument> {
        public AccountProjection(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(
                database,
                AccountProjectionSubscription.SubscriptionGroup,
                loggerFactory
            ) { }

        protected override ValueTask<Operation<AccountDocument>> GetUpdate(object evt)
            => evt switch {
                V1.AccountOpened opened => UpdateOperationTask(
                    filter => filter.Eq(x => x.Id, opened.AccountId),
                    update => update
                        .SetOnInsert(x => x.Id, opened.AccountId)
                        .SetOnInsert(x => x.CustomerId, opened.CustomerId)
                ),
                V1.TransactionInitiated init => UpdateOperationTask(
                    filter => filter.Eq(x => x.Id, init.AccountId),
                    update => update
                        .Set(x => x.AvailableBalance, init.AvailableBalance)
                        .AddToSet(
                            x => x.Transactions,
                            new AccountTransaction(
                                init.TransactionId,
                                init.Amount,
                                TransactionStatus.Initiated
                            )
                        )
                ),
                V1.TransactionBooked booked => UpdateOperationTask(
                    filter => filter
                        .And(
                            Filter.Eq(x => x.Id, booked.AccountId),
                            Filter.ElemMatch(x => x.Transactions, x => x.TransactionId == booked.TransactionId)
                        ),
                    update => update.Set(x => x.Transactions[-1].Status, TransactionStatus.Booked)
                ),
                V1.TransactionCancelled cancelled => NoOp,
                _ => NoOp
            };
    }

    public record AccountDocument : ProjectedDocument {
        public string CustomerId { get; init; }

        public decimal AvailableBalance { get; init; }

        public decimal BookedBalance { get; init; }

        public List<AccountTransaction> Transactions { get; init; } = new();

        public AccountDocument(string Id) : base(Id)
        {
        }
    }

    public enum TransactionStatus {
        Initiated,
        Booked,
        Denied
    }

    public record AccountTransaction(
        string TransactionId, decimal Amount, TransactionStatus Status
    );
}