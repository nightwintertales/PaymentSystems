using System.Threading.Tasks;

namespace PaymentSystems.FrameWork
{
     public interface IProjection
    {
        Task Project(object @event);
    }
}