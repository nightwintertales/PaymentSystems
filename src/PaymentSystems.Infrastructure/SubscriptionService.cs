using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using Microsoft.Extensions.Hosting;

namespace PaymentSystems.Infrastructure {
    public interface IEventHandler {
        Task HandleEvent(object @event);
    }

    public class SubscriptionService : IHostedService {
        readonly EventStoreClient _eventStoreClient;
        readonly ICheckpointStore _checkpointStore;
        readonly IEventHandler[]  _eventHandlers;
        readonly string           _checkpointId;

        StreamSubscription _subscription;

        public SubscriptionService(
            EventStoreClient       eventStoreClient,
            ICheckpointStore       checkpointStore,
            string                 checkpointId,
            params IEventHandler[] eventHandlers
        ) {
            _eventStoreClient = eventStoreClient;
            _checkpointStore  = checkpointStore;
            _checkpointId     = checkpointId;
            _eventHandlers    = eventHandlers;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            var (_, l) = await _checkpointStore.GetCheckpoint(_checkpointId, cancellationToken);

            var position = l != null
                ? new Position((ulong) l.Value, (ulong) l.Value)
                : Position.Start;

            _subscription = await _eventStoreClient.SubscribeToAllAsync(
                position,
                Handler,
                filterOptions: new SubscriptionFilterOptions(EventTypeFilter.ExcludeSystemEvents()),
                cancellationToken: cancellationToken
            );
        }

        async Task Handler(StreamSubscription sub, ResolvedEvent re, CancellationToken cancellationToken) {
            var evt = re.Deserialize();
            await Task.WhenAll(_eventHandlers.Select(x => x.HandleEvent(evt)));

            var checkpoint = new Checkpoint(_checkpointId, (long?) re.Event.Position.CommitPosition);
            await _checkpointStore.StoreCheckpoint(checkpoint, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _subscription.Dispose();
            return Task.CompletedTask;
        }
    }
}
