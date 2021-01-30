namespace PaymentSystems.Domain.Transactions
{
    public class Transaction
    {
         private string _reason;
         private decimal _amount;
         private string _transactionId;
         private string _AccountId;

         public void InitiateTransaction(decimal amount)
         {

         }

         public void BookTransaction(decimal amount, string reason)
         {

         }

         public void CancelTransaction(decimal amount)
         {

         }
    }
}