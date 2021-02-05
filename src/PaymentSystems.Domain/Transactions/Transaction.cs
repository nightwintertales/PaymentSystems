namespace PaymentSystems.Domain.Transactions
{
    public class Transaction : AggregateRoot<TransactionId> 
    {
         private string _reason;
         private decimal _amount;
         private string _transactionId;
         private string _AccountId;

         public void InitiateTransaction(TransactionId id, Amount amount, DateTimeOffset  submittedAt)
         {

         }

         public void BookTransaction(Amount amount, DateTimeOffset  bookedAt, string reason)
         {

         }

         public void CancelTransaction(Amount amount, DateTimeOffset  cancelledAt, string reason)
         {

         }

         protected override void When(object @event) 
         {
            switch (@event) 
            {
            
               
            }
         }
    }
}