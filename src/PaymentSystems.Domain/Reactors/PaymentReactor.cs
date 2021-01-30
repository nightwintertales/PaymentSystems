namespace PaymentSystems.Domain.Reactors
{
    public class PaymentReactor : IEventHandler
    {
        Task HandleEvent(object @event);
        {
                case V1.TransactionInitiated x:
                   // Create an entry in Mongo
                    break;
                case V1.TransactionBooked x:
                 // Update the entry
                    break;

                case V1.TransactionBooked x:
                // Update status
                    break;
        }
    }
}

