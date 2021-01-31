namespace PaymentSystems.Domain.Events
{
    public static class PaymentEvents
    { 
        public class V1
        {
            public class PaymentSubmitted
            {
                public string PaymentId {get;set;}
                public string AccountId {get;set}
                public decimal Ammount {get;set;}
                public string Name {get;set;}
            }

            public class PaymentExecuted
            {
                 public string PaymentId {get;set;}
                 public decimal Ammount {get;set;}
                 public string Name {get;set;}
                 public string AccountId {get;set}
            }

            public class PaymentApproved
            {
                 public string PaymentId {get;set;}
                 public string AccountId {get;set}
                 public decimal Ammount {get;set;}
                 public string Name {get;set;}
            }
        }
    }
}