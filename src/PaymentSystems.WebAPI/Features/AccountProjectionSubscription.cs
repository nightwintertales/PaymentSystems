using System.Collections.Generic;
using EventStore.Client;
using MongoDB.Driver;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Features.Accounts;
using PaymentSystems.WebAPI.Infrastructure;

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