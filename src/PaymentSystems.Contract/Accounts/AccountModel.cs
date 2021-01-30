using System.Linq;

namespace PaymentSystems.Contract.Accounts
{
    public  class AccountModel
    {
        public decimal Balance {get;set;}

        //public IEnumerable<Transaction> Transactions {get;set;}
    }

    public class Transaction
    {
        public decimal Amount {get;set;}
        public string Name {get;set;}
    }
}