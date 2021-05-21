using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Client;
using Eventuous;

namespace PaymentSystems.WebAPI.Infrastructure
{
    public class EsDbAggregateStore : IAggregateStore {
        readonly EventStoreClient _client;

        public EsDbAggregateStore(EventStoreClient client) => _client = client;

        /*
        public async Task Store<T, TId, TState>(T aggregate, CancellationToken cancellationToken)
            where T : Aggregate<TState, TId>, new() where TId : AggregateId where TState : AggregateState<TState, TId>, new()  {
            var expectNew = aggregate.CurrentVersion == -1;

            var stream = StreamName<T, TId, TState>(aggregate);

            var dbEvents = aggregate.Changes
                .Select(
                    x => new EventData(
                        Uuid.NewUuid(),
                        TypeMap.GetTypeName(x.GetType()),
                        JsonSerializer.SerializeToUtf8Bytes(x)
                    )
                );

            var op = expectNew
                ? _client.AppendToStreamAsync(stream, StreamState.NoStream, dbEvents, cancellationToken: cancellationToken)
                : _client.AppendToStreamAsync(
                    stream,
                    StreamRevision.FromInt64(aggregate.CurrentVersion),
                    dbEvents,
                    cancellationToken: cancellationToken
                );

            await op;
        }

*/
        public async Task<T> Load<T, TId, TState>(TId id, CancellationToken cancellationToken)
            where T : Aggregate<TState, TId>, new() where TId : AggregateId where TState : AggregateState<TState, TId>, new() {
            var stream = StreamName<T, TId, TState>(id);

            var read = _client.ReadStreamAsync(
                Direction.Forwards,
                stream,
                StreamPosition.Start,
                cancellationToken: cancellationToken
            );

            var aggregate = new T();

            await foreach (var e in read) {
                var evt = e.Deserialize();
                aggregate.State = aggregate.(evt) with {Version = aggregate.CurrentVersion + 1};
            }

            return aggregate;
        }

/*
        static string StreamName<T, TId, TState>(T aggregate)
            where T : Aggregate<TState, TId>, new() where TId : AggregateId where TState : AggregateState<TState, TId>, new()
            => $"{typeof(T).Name}-{aggregate.State}";
*/
        string StreamName<T>(T aggregate)
            where T : Aggregate
            => $"{typeof(T).Name}-{aggregate}";

        public async Task Store<T>(T entity, CancellationToken cancellationToken) where T : Aggregate
        {
            var stream = StreamName(entity);
            var expectNew = entity.CurrentVersion == -1;

            var dbEvents = entity.Changes
                .Select(
                    x => new EventData(
                        Uuid.NewUuid(),
                        TypeMap.GetTypeName(x.GetType()),
                        JsonSerializer.SerializeToUtf8Bytes(x)
                    )
                );

            var op = expectNew
                ? _client.AppendToStreamAsync(stream, StreamState.NoStream, dbEvents, cancellationToken: cancellationToken)
                : _client.AppendToStreamAsync(
                    stream,
                    StreamRevision.FromInt64(entity.CurrentVersion),
                    dbEvents,
                    cancellationToken: cancellationToken
                );

            await op;
        }

        public async Task<T> Load<T>(string id, CancellationToken cancellationToken) where T : Aggregate, new()
        {
            var stream = StreamName(typeof(T));

            var read = _client.ReadStreamAsync(
                Direction.Forwards,
                stream,
                StreamPosition.Start,
                cancellationToken: cancellationToken
            );

            var aggregate = new T();

            await foreach (var e in read) {
                var evt = e.Deserialize();
                aggregate.State = aggregate.(evt) with {Version = aggregate.CurrentVersion + 1};
            }

            return aggregate;
        }
    }

}