using System;
using System.Threading;
using System.Threading.Tasks;
using Eventuous.Subscriptions;
using Eventuous.Projections.MongoDB;
using MongoDB.Driver;

namespace PaymentSystems.WebAPI.Infrastructure {
    public abstract class MongoProjection<T> : IEventHandler
    {
        readonly IMongoCollection<T> _collection;

        protected MongoProjection(IMongoDatabase database, string subscriptionGroup) {
            SubscriptionGroup = subscriptionGroup;
            _collection       = database.GetCollection<T>("What NAme?");
        }

        public string SubscriptionGroup { get; }

        public Task HandleEvent(object evt, long? position, CancellationToken cancellationToken) {
            var update = GetUpdate(evt);
            if (update == null) return Task.CompletedTask;

            var finalUpdate = update.Update      //Set(x => x., position);

            return _collection.UpdateOneAsync(
                update.Filter,
                finalUpdate,
                new UpdateOptions {IsUpsert = true},
                cancellationToken
            );
        }

        public string SubscriptionId { get; }

        protected abstract UpdateOperation<T> GetUpdate(object evt);

        protected UpdateOperation<T> Operation(
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update
        )
            => new(filter(Builders<T>.Filter), update(Builders<T>.Update));
    }

    public record UpdateOperation<T>(FilterDefinition<T> Filter, UpdateDefinition<T> Update);
}