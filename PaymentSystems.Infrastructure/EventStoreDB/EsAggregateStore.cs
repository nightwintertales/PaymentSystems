using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;

namespace PaymentSystems.Infrastructure.EventStoreDB
{
    public class EsAggregateStore : IAggregateStore
    {
        private readonly EventStoreClient _client;

        public EsAggregateStore(EventStoreClient client) => _client = client;

        public async Task Save<T, TId>(T aggregate, CancellationToken cancellationToken) where T : AggregateRoot<TId>
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));

            var streamName = GetStreamName<T, TId>(aggregate);
            var changes = aggregate.GetChanges().ToArray();
            var events  = changes.Select(CreateEventData);

            var resultTask = aggregate.Version < 0
                ? _client.AppendToStreamAsync(streamName, StreamState.NoStream, events, cancellationToken: cancellationToken)
                : _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(aggregate.Version), events, cancellationToken: cancellationToken);
            await resultTask;

            aggregate.ClearChanges();
            
            static EventData CreateEventData(object e) {
                var meta = new EventMetadata {
                    ClrType = e.GetType().FullName
                };

                return new EventData(
                    Uuid.NewUuid(),
                    TypeMap.GetTypeName(e),
                    JsonSerializer.SerializeToUtf8Bytes(e),
                    JsonSerializer.SerializeToUtf8Bytes(meta)
                );
            }
        }

        public async Task<T> Load<T, TId>(TId aggregateId, CancellationToken cancellationToken)
            where T : AggregateRoot<TId>
        {
            if (aggregateId == null)
                throw new ArgumentNullException(nameof(aggregateId));

            var stream = GetStreamName<T, TId>(aggregateId);
            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            var read           = _client.ReadStreamAsync(Direction.Forwards, stream, StreamPosition.Start);
            var resolvedEvents = await read.ToArrayAsync(cancellationToken);
            var events         = resolvedEvents.Select(x => x.Deserialize());

            aggregate!.Load(events);

            return aggregate;
        }

        public async Task<bool> Exists<T, TId>(TId aggregateId)
        {
            var stream = GetStreamName<T, TId>(aggregateId);
            var read   = _client.ReadStreamAsync(Direction.Forwards, stream, StreamPosition.Start);
            var result = await read.ReadState;
            return result == ReadState.Ok;
        }

        private static string GetStreamName<T, TId>(TId aggregateId)
            => $"{typeof(T).Name}-{aggregateId.ToString()}";

        private static string GetStreamName<T, TId>(T aggregate)
            where T : AggregateRoot<TId>
            => $"{typeof(T).Name}-{aggregate.Id.ToString()}";
    }
}
