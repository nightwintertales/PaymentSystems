
namespace PaymentSystems.Domain.Events
{
    public static class TransactionEvents
    { 
        public static class V1
        {
            public class TransactionInitiated
            {
                public string TransactionId {get;set;}
                public string PaymentId {get;set;}
                public string AccountId {get;set;}
                public decimal Amount {get;set;}
            }

            public class TransactionBooked
            {
                 public string TransactionId {get;set;}
                 public string PaymentId {get;set;}
                 public string AccountId {get;set;}
                 public decimal Amount {get;set;}  
            }
        }
    }
}
