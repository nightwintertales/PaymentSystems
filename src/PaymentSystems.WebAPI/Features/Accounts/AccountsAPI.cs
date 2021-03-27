using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentSystems.WebAPI.Application;
using static PaymentSystems.Contract.AccountCommands;

namespace PaymentSystems.WebAPI.Features.Accounts
{
    public class AccountsAPI
    {
        private readonly PaymentCommandService _service;

        public AccountsAPI(PaymentCommandService service) => _service = service;
    

        [HttpPost]
        [Route("createAccount")]
        public Task Book([FromBody] CreateAccount cmd, CancellationToken cancellationToken) 
            => _service.HandleNew(cmd, cancellationToken);

    }
}