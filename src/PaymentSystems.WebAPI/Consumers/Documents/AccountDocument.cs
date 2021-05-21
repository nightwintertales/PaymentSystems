using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystems.WebAPI.Consumers.Documents
{
    // Rename to known account balances
    public class AccountDocument
    {
        // public int AccountId { get; set; } -> Use the Id property
        public decimal AccountBalance { get; set; } // -> AvailableBalance
    }
}
