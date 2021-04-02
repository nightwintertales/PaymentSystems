using MongoDB.Driver;
using PaymentSystems.WebAPI.Consumers.Documents;
using PaymentSystems.WebAPI.Features;
using PaymentSystems.WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PaymentSystems.WebAPI.Integration.Payments.IntegrationPaymentEvents.V1;

namespace PaymentSystems.WebAPI.Consumers.Mongos
{
    public class PaymentIntegration : MongoProjection<PaymentDocument>
    {
        public PaymentIntegration(IMongoDatabase database)
          : base(
              database,
              AccountProjectionSubscription.SubscriptionGroup
          )
        { }

        protected override UpdateOperation<PaymentDocument> GetUpdate(object evt)
            => evt switch
            {
                PaymentApproved opened => Operation(
                    filter => filter.Eq(x => x.PaymentId, opened.PaymentId),
                    update => update
                        .SetOnInsert(x => x.PaymentId, opened.PaymentId)
                        .SetOnInsert(x => x.PaymentAmount, opened.Balance)
                ),
             _ => null
            };
    }
}
