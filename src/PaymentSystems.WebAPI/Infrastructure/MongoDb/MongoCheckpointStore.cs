using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PaymentSystems.FrameWork;
using PaymentSystems.FrameWork.Projections;

namespace PaymentSystems.WebAPI.Infrastructure.MongoDb
{
    public class MongoCheckpointStore : ICheckpointStore {
        readonly int                           _batchSize;
        readonly         ILogger<MongoCheckpointStore> Log;

        public MongoCheckpointStore(IMongoCollection<Checkpoint> database, int batchSize, ILogger<MongoCheckpointStore> logger) {
            Checkpoints     = database;
            _batchSize = batchSize;
            Log             = logger;
        }

        public MongoCheckpointStore(IMongoDatabase database, ILogger<MongoCheckpointStore> logger) : this(database.GetCollection<Checkpoint>("checkpoint"), 0, logger) { }

        IMongoCollection<Checkpoint> Checkpoints { get; }

        public async ValueTask<Checkpoint> GetLastCheckpoint(string checkpointId, CancellationToken cancellationToken = default) {
            Log.LogDebug("[{CheckpointId}] Finding checkpoint...", checkpointId);

            var checkpoint = await Checkpoints.AsQueryable()
                .Where(x => x.Id == checkpointId)
                .SingleOrDefaultAsync(cancellationToken);

            if (checkpoint is null) {
                checkpoint = new Checkpoint(checkpointId, null);
                Log.LogInformation("[{CheckpointId}] Checkpoint not found, defaulting to earliest position", checkpointId);
            }
            else {
                Log.LogInformation("[{CheckpointId}] Checkpoint found at position {Checkpoint}", checkpointId, checkpoint.Position);
            }

            _counters[checkpointId] = 0;

            return checkpoint;
        }

        readonly Dictionary<string, int> _counters = new();

        public async ValueTask<Checkpoint> StoreCheckpoint(Checkpoint checkpoint, CancellationToken cancellationToken = default) {
            _counters[checkpoint.Id]++;
            if (_counters[checkpoint.Id] < _batchSize) return checkpoint;
            
            await Checkpoints.ReplaceOneAsync(
                x => x.Id == checkpoint.Id,
                checkpoint,
                MongoDefaults.DefaultReplaceOptions,
                cancellationToken
            );

            Log.LogDebug("[{CheckpointId}] Checkpoint position set to {Checkpoint}", checkpoint.Id, checkpoint.Position);

            return checkpoint;
        }
    }
}
