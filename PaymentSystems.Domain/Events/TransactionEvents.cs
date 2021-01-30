namespace PaymentSystems.Domain.Events
{
    public static class TransactactionEvents
    { 
        public static class V1
        {
            public class TransactionInitiated
            {
                public string TransactionId {get;set;}
                public string PaymentId {get;set;}
                public decimal Ammount {get;set;}
            }

            public class TransactionBooked
            {
                 public string TransactionId {get;set;}
                 public string PaymentId {get;set;}
                public decimal Ammount {get;set;}
            }

            public class TransactionCancelled
            {
                 public string TransactionId {get;set;}
                 public string PaymentId {get;set;}
                 public decimal Ammount {get;set;}
                 public string Reason {get;set}
            }
        }
    }
}