using System.Threading;
using System.Threading.Tasks;

namespace PaymentSystems.FrameWork
{
    public record Checkpoint(string Id, long? Position);
    
    public interface ICheckpointStore
    {
        ValueTask<Checkpoint> GetCheckpoint(string id, CancellationToken cancellationToken);
        ValueTask StoreCheckpoint(Checkpoint checkpoint, CancellationToken cancellationToken);
    }
}
