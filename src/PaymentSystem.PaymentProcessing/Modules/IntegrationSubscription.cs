using System.Collections.Generic;
using EventStore.Client;
using Eventuous;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.EventStoreDB;
using Microsoft.Extensions.Logging;
using StreamSubscription = Eventuous.Subscriptions.EventStoreDB.StreamSubscription;

namespace PaymentSystem.PaymentProcessing.Modules {
    public class IntegrationSubscription : StreamSubscription {
        public IntegrationSubscription(
            EventStoreClient         eventStoreClient,       
            string streamName, 
            string subscriptionId,
            ICheckpointStore         checkpointStore,        
            IEnumerable<IEventHandler> eventHandlers,
            IEventSerializer?        eventSerializer = null, 
            ILoggerFactory? loggerFactory = null,
            ISubscriptionGapMeasure? measure         = null, 
            bool throwOnError = false
        ) : base(
            eventStoreClient,
            streamName,
            subscriptionId,
            checkpointStore,
            eventHandlers,
            eventSerializer,
            loggerFactory,
            measure,
            throwOnError
        ) { }

    }
}