using System.Threading;
using System.Threading.Tasks;

namespace PaymentSystems.FrameWork
{
    public interface IAggregateStore
    {
        Task<bool> Exists<T, TId>(TId aggregateId);
        
        Task Save<T, TId>(T aggregate, CancellationToken cancellationToken) where T : AggregateRoot<TId>;
        
        Task<T> Load<T, TId>(TId aggregateId, CancellationToken cancellationToken) where T : AggregateRoot<TId>;
    }
}