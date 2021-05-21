using EventStore.Client;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.EventStoreDB;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace PaymentSystems.WebAPI.Features.Accounts {
    public class AccountProjectionSubscription : AllStreamSubscription {
        public const string SubscriptionGroup = "AccountProjections";

        public AccountProjectionSubscription(
            EventStoreClient eventStoreClient,
            ICheckpointStore checkpointStore,
            IMongoDatabase   database,
            ILoggerFactory loggerFactory
        )
            : base(eventStoreClient, SubscriptionGroup, checkpointStore, new[] {new AccountProjection(database, loggerFactory)}) { }
    }
}