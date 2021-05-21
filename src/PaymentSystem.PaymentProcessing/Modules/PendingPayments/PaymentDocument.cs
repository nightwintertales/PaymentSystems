using Eventuous.Projections.MongoDB.Tools;

namespace PaymentSystems.WebAPI.Consumers.Documents
{
    public record PaymentDocument : ProjectedDocument
    {
        // Id from the base class -> Payment id
        public string  AccountId     { get; init; }
        public decimal PaymentAmount { get; init; }
        public PaymentDocument(string id) : base(id) { }
    }
}
