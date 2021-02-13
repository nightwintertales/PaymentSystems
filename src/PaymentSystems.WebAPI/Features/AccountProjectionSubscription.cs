using System.Collections.Generic;
using EventStore.Client;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Infrastructure;

namespace PaymentSystems.WebAPI.Features {
    public class AccountProjectionSubscription : SubscriptionService {
        public const string SubscriptionGroup = "AccountProjections";

        public AccountProjectionSubscription(
            EventStoreClient           eventStoreClient,
            ICheckpointStore           checkpointStore,
            IEnumerable<IEventHandler> projections
        )
            : base(eventStoreClient, checkpointStore, SubscriptionGroup, projections) { }
    }

    public class AccountReactorSubscription : SubscriptionService {
        public const string SubscriptionGroup = "AccountReactors";

        public AccountReactorSubscription(
            EventStoreClient           eventStoreClient,
            ICheckpointStore           checkpointStore,
            IEnumerable<IEventHandler> projections
        ) : base(eventStoreClient, checkpointStore, SubscriptionGroup, projections) { }
    }
}
