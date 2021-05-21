using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentSystems.WebAPI.Application;
using static PaymentSystems.Contract.AccountCommands;

namespace PaymentSystems.WebAPI.Features.Accounts
{
    [ApiController]
    [Route("/payments")]
    public class AccountsAPI : ControllerBase
    {
        private readonly AccountCommandService _service;

        public AccountsAPI(AccountCommandService service) => _service = service;
    

        [HttpPost]
        [Route("createAccount")]
        public Task Book([FromBody] CreateAccount cmd, CancellationToken cancellationToken) 
            => _service.HandleNew(cmd, cancellationToken);

    }
}