using EventStore.Client;
using Eventuous.Subscriptions;
using MongoDB.Driver;
using PaymentSystems.WebAPI.Features.Accounts;
using SubscriptionService = PaymentSystems.WebAPI.Infrastructure.SubscriptionService;

namespace PaymentSystems.WebAPI.Features {
    public class AccountProjectionSubscription : SubscriptionService {
        public const string SubscriptionGroup = "AccountProjections";

        public AccountProjectionSubscription(
            EventStoreClient eventStoreClient,
            ICheckpointStore checkpointStore,
            IMongoDatabase   database
        )
            : base(eventStoreClient, checkpointStore, SubscriptionGroup, new[] {new AccountProjection(database)}) { }
    }
}