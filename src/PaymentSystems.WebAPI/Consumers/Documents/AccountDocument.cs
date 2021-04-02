using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystems.WebAPI.Consumers.Documents
{
    public class AccountDocument
    {
        public int AccountId { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
