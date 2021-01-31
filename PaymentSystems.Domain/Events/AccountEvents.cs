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
                public string PaymentId {get;set;}
                public decimal AmountBalance {get;set;}
                public decimal DisposableAmount {get;set;}
                public decimal BookedAmount {get;set;}
            }

            public class TransactionBooked
            {
                 public string AccountId {get;set;}
                 public string TransactionId {get;set;}
                 public decimal AmountBalance {get;set;}
                 public string PaymentId {get;set;}
                 public decimal DisposableAmount {get;set;}
                 public decimal BookedAmount {get;set;}

            }

            public class AccountOpened
            {
                 public string AccountId {get;set;}
                 public string CustomerId {get;set;}
                 public string Reason {get;set}
            }
        }  
    }
}