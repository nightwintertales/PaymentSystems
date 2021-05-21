using System.Threading.Tasks;
using Eventuous.Projections.MongoDB;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PaymentSystems.WebAPI.Consumers.Documents;
using static PaymentSystems.Domain.Events.PaymentEvents;

namespace PaymentSystem.PaymentProcessing.Modules.PendingPayments {
    public class PaymentIntegration : MongoProjection<PaymentDocument> {
        public PaymentIntegration(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(
                database,
                AccountProjectionSubscription.SubscriptionGroup,
                loggerFactory
            ) { }

        protected override ValueTask<Operation<PaymentDocument>> GetUpdate(object evt)
            => evt switch {
                V1.PaymentApproved opened => UpdateOperationTask(
                    opened.PaymentId,
                    update => update.SetOnInsert(x => x.Id, opened.PaymentId)
                ),
                _ => NoOp
            };
    }
}