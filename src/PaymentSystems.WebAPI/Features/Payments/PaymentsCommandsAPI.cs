using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentSystems.WebAPI.Application;
using static PaymentSystems.Contract.PaymentCommands;

namespace PaymentSystems.WebAPI.Features.Payments
{
    [ApiController]
    [Route("/payments")]
    public class PaymentsCommandsAPI : ControllerBase
    {
        readonly PaymentCommandService _service;

        public PaymentsCommandsAPI(PaymentCommandService service) => _service = service;
    

        [HttpPost]
        [Route("submit")]
        public Task Book([FromBody] Submit cmd, CancellationToken cancellationToken) 
            => _service.HandleNew(cmd, cancellationToken);

        [HttpPost]
        [Route("approve")]
        public Task ConfirmPayment([FromBody] Approve cmd, CancellationToken cancellationToken) 
            => _service.HandleExisting(cmd, cancellationToken);

    }
}