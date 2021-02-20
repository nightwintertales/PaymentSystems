using System.Threading;
using System.Threading.Tasks;

namespace PaymentSystems.FrameWork.Projections {
    public interface IEventHandler {
        string SubscriptionGroup { get; }
        
        Task HandleEvent(object evt, long? position, CancellationToken cancellationToken);
    }
}
