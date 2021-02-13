using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using PaymentSystems.FrameWork;
using PaymentSystems.FrameWork.Projections;
using PaymentSystems.WebAPI.Infrastructure.MongoDb;

namespace PaymentSystems.WebAPI.Infrastructure {
    public abstract class MongoProjection<T> : IEventHandler where T : Document {
        readonly IMongoCollection<T> _collection;

        protected MongoProjection(IMongoDatabase database, string subscriptionGroup) {
            SubscriptionGroup = subscriptionGroup;
            _collection       = database.GetDocumentCollection<T>();
        }

        public string SubscriptionGroup { get; }

        public async Task HandleEvent(object evt, long? position) {
            var update = GetUpdate(evt);
            if (update == null) return;

            var finalUpdate = update.Update.Set(x => x.Position, position);

            await _collection.UpdateOneAsync(update.Filter, finalUpdate, new UpdateOptions {IsUpsert = true});
        }

        protected abstract UpdateOperation<T> GetUpdate(object evt);

        protected UpdateOperation<T> Operation(
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update
        )
            => new(filter(Builders<T>.Filter), update(Builders<T>.Update));
    }

    public record UpdateOperation<T>(FilterDefinition<T> Filter, UpdateDefinition<T> Update);
}
