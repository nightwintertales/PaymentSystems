using System.Threading;
using System.Threading.Tasks;

namespace PaymentSystems.FrameWork.Projections {
    public record Checkpoint(string Id, long? Position);

    public interface ICheckpointStore {
        ValueTask<Checkpoint> GetLastCheckpoint(string checkpointId, CancellationToken cancellationToken = default);

        ValueTask<Checkpoint> StoreCheckpoint(Checkpoint checkpoint, CancellationToken cancellationToken = default);
    }
}
