using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using Microsoft.Extensions.Hosting;
using PaymentSystems.FrameWork.Projections;

namespace PaymentSystems.WebAPI.Infrastructure {
    public abstract class SubscriptionService : IHostedService {
        readonly EventStoreClient _eventStoreClient;
        readonly ICheckpointStore _checkpointStore;
        readonly IEventHandler[]  _eventHandlers;
        readonly string           _subscriptionName;

        StreamSubscription _subscription;
        long?              _lastProcessedPosition;

        protected SubscriptionService(
            EventStoreClient           eventStoreClient,
            ICheckpointStore           checkpointStore,
            string                     subscriptionName,
            IEnumerable<IEventHandler> eventHandlers
        ) {
            _eventStoreClient = eventStoreClient;
            _checkpointStore  = checkpointStore;
            _subscriptionName = subscriptionName;

            _eventHandlers = eventHandlers
                .Where(x => x.SubscriptionGroup == subscriptionName)
                .ToArray();
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            var checkpoint = await _checkpointStore.GetLastCheckpoint(
                _subscriptionName,
                cancellationToken
            );

            var position = checkpoint.Position != null
                ? new Position((ulong) checkpoint.Position.Value, (ulong) checkpoint.Position.Value)
                : Position.Start;
            _lastProcessedPosition = checkpoint.Position;

            _subscription = await _eventStoreClient.SubscribeToAllAsync(
                position,
                Handler,
                filterOptions: new SubscriptionFilterOptions(
                    EventTypeFilter.ExcludeSystemEvents(),
                    10,
                    (_, pos, ct) => StoreCheckpoint(pos.CommitPosition, ct)
                ),
                cancellationToken: cancellationToken
            );
        }

        async Task Handler(
            StreamSubscription sub, ResolvedEvent re, CancellationToken cancellationToken
        ) {
            _lastProcessedPosition = (long?) re.Event.Position.CommitPosition;

            var evt = re.Deserialize();

            if (evt != null) {
                await Task.WhenAll(
                    _eventHandlers.Select(
                        x => x.HandleEvent(evt, (long?) re.OriginalPosition?.CommitPosition, cancellationToken)
                    )
                );
            }

            await StoreCheckpoint(re.Event.Position.CommitPosition, cancellationToken);
        }

        async Task StoreCheckpoint(ulong position, CancellationToken cancellationToken) {
            _lastProcessedPosition = (long?) position;
            var checkpoint = new Checkpoint(_subscriptionName, _lastProcessedPosition);
            await _checkpointStore.StoreCheckpoint(checkpoint, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _subscription.Dispose();
            return Task.CompletedTask;
        }
    }
}