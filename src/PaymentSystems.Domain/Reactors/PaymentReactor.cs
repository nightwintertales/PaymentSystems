using PaymentSystems.Domain.Events;

namespace PaymentSystems.Domain.Reactors
{
    public class PaymentReactor : IEventHandler
    {
        Task HandleEvent(object @event)
        {
            switch (@event)
            {
                case V1.PaymentSubmitted x:
                   // Create an entry in Mongo
                    break;
            }
               
        }
    }
}

