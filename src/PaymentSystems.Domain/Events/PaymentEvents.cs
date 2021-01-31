<<<<<<< HEAD
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
=======
namespace PaymentSystems.Domain.Events {
    public static class PaymentEvents {
        public class V1 {
            public class PaymentSubmitted {
                public string  PaymentId { get; set; }
                public decimal Amount    { get; set; }
>>>>>>> 76acc8081b4cb99a1e4a796daaee04bafeb84e19
            }

            public class PaymentExecuted
            {
                 public string PaymentId {get;set;}
                 public decimal Ammount {get;set;}
                 public string Name {get;set;}
                 public string AccountId {get;set}
            }

<<<<<<< HEAD
            public class PaymentApproved
            {
                 public string PaymentId {get;set;}
                 public string AccountId {get;set}
                 public decimal Ammount {get;set;}
                 public string Name {get;set;}
            }
=======
            public class PaymentApproved {
                public string  PaymentId { get; set; }
                public decimal Amount    { get; set; }
                public string Name {get;set;}
            }

            //Bank
            public class PaymentExecuted {
                public string  PaymentId { get; set; }
                public decimal Amount    { get; set; }
            }

            public class PaymentRegistered 
            {
                 public string PaymentId {get;set;}
                 public decimal Amount {get;set;}
                 public string Name {get;set;}
            }

            //Processing center
            public class PaymentConfirmed 
            
            {
                 public string PaymentId {get;set;}
                 public decimal Amount {get;set;}
                 public string Name {get;set;}
            }

            public class PaymentFailed {
                public string  PaymentId { get; set; }
                public decimal Amount   { get; set; }
            }
>>>>>>> 76acc8081b4cb99a1e4a796daaee04bafeb84e19
        }
    }
}
