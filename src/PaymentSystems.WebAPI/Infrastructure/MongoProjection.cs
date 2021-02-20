using System;
using System.Threading;
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

        public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken) {
            var update = GetUpdate(evt);
            if (update == null) return Task.CompletedTask;

            var finalUpdate = update.Update.Set(x => x.Position, position);

            return _collection.UpdateOneAsync(
                update.Filter,
                finalUpdate,
                new UpdateOptions {IsUpsert = true},
                cancellationToken
            );
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