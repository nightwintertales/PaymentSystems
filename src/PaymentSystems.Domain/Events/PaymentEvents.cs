using PaymentSystems.Domain.Payments;

namespace PaymentSystems.Domain.Events
{
    public static class PaymentEvents
    { 
        public static class V1 {
            public class PaymentSubmitted {
                public string  AccountId     { get; set; }
                public string  PaymentId { get; set; }
                public decimal Amount {get;set;}
                public decimal Name   { get; set; }
                public PaymentStatus Status => PaymentStatus.Submitted;
            }

            public class PaymentApproved {
                public string  AccountId     { get; set; }
                public string  PaymentId { get; set; }
                public decimal Amount {get;set;}
                public decimal Name   { get; set; }
                public PaymentStatus Status => PaymentStatus.Approved;
            }

            public class PaymentExecuted {
                public string  AccountId     { get; set; }
                public string  PaymentId { get; set; }
                public decimal Amount {get;set;}
                public decimal Name   { get; set; }
                public PaymentStatus Status => PaymentStatus.Executed;
            }
        }
        
    }
}
