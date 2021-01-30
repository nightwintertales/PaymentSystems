namespace PaymentSystems.Domain.Events
{
    public static class AccountEvents
    {
        public static class V1
        {
            public class TransactionInitiated
            {
                public string AccountId {get;set;}
                public string TransactionId {get;set;}
                public decimal Ammount {get;set;}
            }

            public class TransactionBooked
            {
                 public string AccountId {get;set;}
                 public string TransactionId {get;set;}
                 public decimal Ammount {get;set;}
            }

            public class TransactionCancelled
            {
                 public string AccountId {get;set;}
                 public string TransactionId {get;set;}
                 public decimal Ammount {get;set;}
                 public string Reason {get;set}
            }
        }  
    }
}