using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.Client;
using Eventuous;
using Eventuous.Projections.MongoDB;
using Eventuous.Projections.MongoDB.Tools;
using Eventuous.Subscriptions;
using Eventuous.Subscriptions.EventStoreDB;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using static PaymentSystems.Domain.Events.PaymentEvents;

namespace PaymentSystems.WebAPI.Features.Payments {
    public class PaymentsProjectionSubscription : AllStreamSubscription {
        const string Id = "PaymentsProjectionSubscription";

        public PaymentsProjectionSubscription(
            EventStoreClient eventStoreClient,
            ICheckpointStore checkpointStore,
            IEventSerializer eventSerializer,
            IMongoDatabase   database,
            ILoggerFactory   loggerFactory
        ) : base(
            eventStoreClient,
            Id,
            checkpointStore,
            new[] {new PaymentsProjection(database, loggerFactory)},
            eventSerializer,
            loggerFactory
        ) { }

        class PaymentsProjection : MongoProjection<PaymentDocument> {
            public PaymentsProjection(
                IMongoDatabase database,
                ILoggerFactory loggerFactory
            )
                : base(database, Id, loggerFactory) { }

            protected override ValueTask<Operation<PaymentDocument>> GetUpdate(object evt) {
                return evt switch {
                    V1.PaymentSubmitted e => UpdateOperationTask(
                        e.PaymentId,
                        update => update
                            .SetOnInsert(x => x.Id, e.PaymentId)
                            .Set(x => x.Amount, e.Amount)
                            .Set(x => x.Message, e.Message)
                            .Set(x => x.AccountId, e.AccountId)
                            .Set(
                                x => x.Payee,
                                new Payee(
                                    e.Payee.Name,
                                    e.Payee.AccountDetails.SortCode,
                                    e.Payee.AccountDetails.AccountNumber
                                )
                            )
                    ),
                    V1.PaymentApproved e => UpdateOperationTask(
                        e.PaymentId,
                        update => update.Set(x => x.Approved, true)
                    ),
                    _ => NoOp
                };
            }
        }
    }

    record PaymentDocument : ProjectedDocument {
        public PaymentDocument(string id) : base(id) { }
        public string  AccountId { get; set; }
        public decimal Amount    { get; set; }
        public Payee   Payee     { get; init; }
        public string  Message   { get; init; }
        public bool    Approved  { get; init; }
    }

    record Payee(string Name, string SortCode, string AccountNumber);
}