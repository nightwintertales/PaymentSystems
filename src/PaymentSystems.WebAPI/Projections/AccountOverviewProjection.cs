namespace PaymentSystems.WebAPI.Projections
{
    public class AccountOverviewProjection : Projection
    {
        
        public override async Task Handle(object e)
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