using System.Collections.Generic;
using Eventuous.Projections.MongoDB.Tools;
using MongoDB.Driver;
using PaymentSystems.WebAPI.Infrastructure;
using static MongoDB.Driver.Builders<PaymentSystems.WebAPI.Features.Accounts.AccountDocument>;
using static PaymentSystems.Domain.Accounts.AccountEvents;

namespace PaymentSystems.WebAPI.Features.Accounts {
    public class AccountProjection : MongoProjection<AccountDocument> {
        public AccountProjection(IMongoDatabase database)
            : base(
                database,
                AccountProjectionSubscription.SubscriptionGroup
            ) { }

        protected override UpdateOperation<AccountDocument> GetUpdate(object evt)
            => evt switch {
                V1.AccountOpened opened => Operation(
                    filter => filter.Eq(x => x.Id, opened.AccountId),
                    update => update
                        .SetOnInsert(x => x.Id, opened.AccountId)
                        .SetOnInsert(x => x.CustomerId, opened.CustomerId)
                ),
                V1.TransactionInitiated init => Operation(
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
                V1.TransactionBooked booked => Operation(
                    filter => filter
                        .And(
                            Filter.Eq(x => x.Id, booked.AccountId),
                            Filter.ElemMatch(x => x.Transactions, x => x.TransactionId == booked.TransactionId)
                        ),
                    update => update.Set(x => x.Transactions[-1].Status, TransactionStatus.Booked)
                ),
                V1.TransactionCancelled cancelled => null,
                _ => null
            };
    }

    public record AccountDocument : Document {
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