using MongoDB.Driver;
using PaymentSystems.FrameWork;

namespace PaymentSystems.WebAPI.Infrastructure.MongoDb {
    public static class MongoExtensions {
        public static FilterDefinition<T> IdEquals<T>(this FilterDefinitionBuilder<T> filter, string id) where T : Document
            => filter.Eq(x => x.Id, id);
    }
}
