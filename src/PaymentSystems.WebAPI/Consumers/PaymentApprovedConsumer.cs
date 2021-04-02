using MassTransit;
using PaymentSystems.WebAPI.Consumers.Mongos;
using PaymentSystems.WebAPI.Features.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PaymentSystems.WebAPI.Integration.Payments.IntegrationPaymentEvents.V1;

namespace PaymentSystems.WebAPI.Consumers
{
    public class PaymentApprovedConsumer : IConsumer<PaymentApproved>
    {
        public async Task Consume(ConsumeContext<PaymentApproved> context)
        {
            var payment = context.Message;
            //API to save in Mongo - Please, review the document
           
        }
    }
}
