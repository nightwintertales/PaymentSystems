namespace PaymentSystems.Domain.Events {
    public static class PaymentEvents {
        public class V1 {
            public class PaymentSubmitted {
                public string  PaymentId { get; set; }
                public decimal Amount    { get; set; }
            }

            public class PaymentCancelled
            {
                 public string PaymentId {get;set;}
                 public decimal Ammount {get;set;}
                 public string Name {get;set;}
            }

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
        }
    }
}
