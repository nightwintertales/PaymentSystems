using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eventuous.Subscriptions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace PaymentSystems.WebAPI.Infrastructure.MongoDb {
    public class MongoCheckpointStore : ICheckpointStore {
        readonly int                           _batchSize;
        readonly ILogger<MongoCheckpointStore> _log;

        public MongoCheckpointStore(
            IMongoCollection<Checkpoint> database, int batchSize, ILogger<MongoCheckpointStore> logger
        ) {
            Checkpoints = database;
            _batchSize  = batchSize;
            _log        = logger;
        }

        public MongoCheckpointStore(IMongoDatabase database, ILogger<MongoCheckpointStore> logger) : this(
            database.GetCollection<Checkpoint>("checkpoint"),
            0,
            logger
        ) { }

        IMongoCollection<Checkpoint> Checkpoints { get; }

        public async ValueTask<Checkpoint> GetLastCheckpoint(
            string checkpointId, CancellationToken cancellationToken = default
        ) {
            _log.LogDebug("[{CheckpointId}] Finding checkpoint...", checkpointId);

            var checkpoint = await Checkpoints.AsQueryable()
                .Where(x => x.Id == checkpointId)
                .SingleOrDefaultAsync(cancellationToken);

            if (checkpoint is null) {
                checkpoint = new Checkpoint(checkpointId, null);

                _log.LogInformation(
                    "[{CheckpointId}] Checkpoint not found, defaulting to earliest position",
                    checkpointId
                );
            }
            else {
                _log.LogInformation(
                    "[{CheckpointId}] Checkpoint found at position {Checkpoint}",
                    checkpointId,
                    checkpoint.Position
                );
            }

            _counters[checkpointId] = 0;

            return checkpoint;
        }

        readonly Dictionary<string, int> _counters = new();

        public async ValueTask<Checkpoint> StoreCheckpoint(
            Checkpoint checkpoint, CancellationToken cancellationToken = default
        ) {
            _counters[checkpoint.Id]++;
            if (_counters[checkpoint.Id] < _batchSize) return checkpoint;

            await Checkpoints.ReplaceOneAsync(
                x => x.Id == checkpoint.Id,
                checkpoint,
                MongoDefaults.DefaultReplaceOptions,
                cancellationToken
            );

            _log.LogDebug(
                "[{CheckpointId}] Checkpoint position set to {Checkpoint}",
                checkpoint.Id,
                checkpoint.Position
            );

            return checkpoint;
        }
    }
}