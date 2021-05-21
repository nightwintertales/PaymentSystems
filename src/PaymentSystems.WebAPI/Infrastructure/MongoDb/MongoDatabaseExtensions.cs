using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Eventuous.Projections.MongoDB.Tools;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace PaymentSystems.WebAPI.Infrastructure.MongoDb {
    
    public static class MongoDatabaseExtensions {
        public static Task<bool> DocumentExists<T>(
            this IMongoDatabase database,
            string              id,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.DocumentExists(database.GetDocumentCollection<T>(), id, cancellationToken);

        public static Task<T> LoadDocument<T>(
            this IMongoDatabase database,
            string              id,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocument(database.GetDocumentCollection<T>(), id, cancellationToken);

        public static Task<TResult> LoadDocumentAs<T, TResult>(
            this IMongoDatabase          database,
            string                       id,
            Expression<Func<T, TResult>> projection,
            CancellationToken            cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentAs(database.GetDocumentCollection<T>(), id, projection, cancellationToken);

        public static Task<List<T>> LoadDocuments<T>(
            this IMongoDatabase database,
            IEnumerable<string> ids,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocuments(database.GetDocumentCollection<T>(), ids, cancellationToken);

        public static Task<List<TResult>> LoadDocumentsAs<T, TResult>(
            this IMongoDatabase          database,
            IEnumerable<string>          ids,
            Expression<Func<T, TResult>> projection,
            CancellationToken            cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentsAs(database.GetDocumentCollection<T>(), ids, projection, cancellationToken);

        public static Task<TResult> LoadDocumentAs<T, TResult>(
            this IMongoDatabase              database,
            string                           id,
            ProjectionDefinition<T, TResult> projection,
            CancellationToken                cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentAs(database.GetDocumentCollection<T>(), id, projection, cancellationToken);

        public static Task<TResult> LoadDocumentAs<T, TResult>(
            this IMongoDatabase                                           database,
            string                                                        id,
            Func<ProjectionDefinitionBuilder<T>, ProjectionDefinition<T>> projection,
            CancellationToken                                             cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentAs<T, TResult>(database.GetDocumentCollection<T>(), id, projection, cancellationToken);

        /// <summary>
        /// Replaces the document or inserts a new one if no matching document by id is found.
        /// </summary>
        public static Task StoreDocument<T>(
            this IMongoDatabase database,
            T                   document,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.ReplaceDocument(database.GetDocumentCollection<T>(), document, cancellationToken);

        /// <summary>
        /// Replaces the document or inserts a new one if no matching document by id is found.
        /// </summary>
        public static Task ReplaceDocument<T>(
            this IMongoDatabase database,
            T                   document,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.ReplaceDocument(database.GetDocumentCollection<T>(), document, cancellationToken);

        /// <summary>
        /// Replaces the document or inserts a new one if no matching document by id is found.
        /// </summary>
        public static Task<ReplaceOneResult> ReplaceDocument<T>(
            this IMongoDatabase    database,
            T                      document,
            Action<ReplaceOptions> configure,
            CancellationToken      cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.ReplaceDocument(database.GetDocumentCollection<T>(), document, configure, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase   database,
            FilterDefinition<T>   filter,
            UpdateDefinition<T>   update,
            Action<UpdateOptions> configure,
            CancellationToken     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateDocument(database.GetDocumentCollection<T>(), filter, update, configure, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase                                   database,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            Action<UpdateOptions>                                 configure,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).UpdateDocument(filter, update, configure, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase database,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateDocument(database.GetDocumentCollection<T>(), filter, update, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase                                   database,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).UpdateDocument(filter, update, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase                                   database,
            string                                                id,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            Action<UpdateOptions>                                 configure,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).UpdateDocument(id, update, configure, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase                                   database,
            string                                                id,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).UpdateDocument(id, update, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase database,
            string              id,
            UpdateDefinition<T> update,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateDocument(database.GetDocumentCollection<T>(), id, update, cancellationToken);

        public static Task<long> UpdateManyDocuments<T>(
            this IMongoDatabase                                   database,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).UpdateManyDocuments(filter, update, cancellationToken);

        public static Task<long> UpdateManyDocuments<T>(
            this IMongoDatabase database,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateManyDocuments(database.GetDocumentCollection<T>(), filter, update, cancellationToken);

        public static Task<long> UpdateManyDocuments<T>(
            this IMongoDatabase                                   database,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            Action<UpdateOptions>                                 configure,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).UpdateManyDocuments(filter, update, configure, cancellationToken);

        public static Task<long> UpdateManyDocuments<T>(
            this IMongoDatabase   database,
            FilterDefinition<T>   filter,
            UpdateDefinition<T>   update,
            Action<UpdateOptions> configure,
            CancellationToken     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateManyDocuments(database.GetDocumentCollection<T>(), filter, update, configure, cancellationToken);

        public static Task<bool> DeleteDocument<T>(
            this IMongoDatabase database,
            string              id,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.DeleteDocument(database.GetDocumentCollection<T>(), id, cancellationToken);

        public static Task<long> DeleteManyDocuments<T>(
            this IMongoDatabase                                   database,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).DeleteManyDocuments(filter, cancellationToken);

        public static Task<long> DeleteManyDocuments<T>(
            this IMongoDatabase database,
            FilterDefinition<T> filter,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.DeleteManyDocuments(database.GetDocumentCollection<T>(), filter, cancellationToken);

        public static Task<long> BulkUpdateDocuments<T>(
            this IMongoDatabase                                      database,
            IEnumerable<T>                                           documents,
            Func<T, FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<T, UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            Action<BulkWriteOptions>                                 configure,
            CancellationToken                                        cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).BulkUpdateDocuments(documents, filter, update, configure, cancellationToken);

        public static Task<long> BulkUpdateDocuments<T>(
            this IMongoDatabase                                      database,
            IEnumerable<T>                                           documents,
            Func<T, FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<T, UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            CancellationToken                                        cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).BulkUpdateDocuments(documents, filter, update, cancellationToken);

        public static Task<bool> DocumentExists<T>(
            this IMongoDatabase database,
            string              id,
            MongoCollectionName collectionName,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.DocumentExists(database.GetDocumentCollection<T>(collectionName), id, cancellationToken);

        public static Task<T> LoadDocument<T>(
            this IMongoDatabase database,
            string              id,
            MongoCollectionName collectionName,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocument(database.GetDocumentCollection<T>(collectionName), id, cancellationToken);

        public static Task<TResult> LoadDocumentAs<T, TResult>(
            this IMongoDatabase          database,
            string                       id,
            Expression<Func<T, TResult>> projection,
            MongoCollectionName          collectionName,
            CancellationToken            cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentAs(database.GetDocumentCollection<T>(collectionName), id, projection, cancellationToken);

        public static Task<TResult> LoadDocumentAs<T, TResult>(
            this IMongoDatabase              database,
            string                           id,
            ProjectionDefinition<T, TResult> projection,
            MongoCollectionName              collectionName,
            CancellationToken                cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentAs(database.GetDocumentCollection<T>(collectionName), id, projection, cancellationToken);

        public static Task<TResult> LoadDocumentAs<T, TResult>(
            this IMongoDatabase                                           database,
            string                                                        id,
            Func<ProjectionDefinitionBuilder<T>, ProjectionDefinition<T>> projection,
            MongoCollectionName                                           collectionName,
            CancellationToken                                             cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.LoadDocumentAs<T, TResult>(database.GetDocumentCollection<T>(collectionName), id, projection, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase                                   database,
            string                                                id,
            MongoCollectionName                                   collectionName,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => database.GetDocumentCollection<T>(collectionName).UpdateDocument(id, update, cancellationToken);

        public static Task UpdateDocument<T>(
            this IMongoDatabase database,
            string              id,
            MongoCollectionName collectionName,
            UpdateDefinition<T> update,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateDocument(database.GetDocumentCollection<T>(collectionName), id, update, cancellationToken);

        public static Task<long> UpdateManyDocuments<T>(
            this IMongoDatabase                                   database,
            MongoCollectionName                                   collectionName,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => database.GetDocumentCollection<T>(collectionName).UpdateManyDocuments(filter, update, cancellationToken);

        public static Task<long> UpdateManyDocuments<T>(
            this IMongoDatabase database,
            MongoCollectionName collectionName,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.UpdateManyDocuments(database.GetDocumentCollection<T>(collectionName), filter, update, cancellationToken);

        public static Task<bool> DeleteDocument<T>(
            this IMongoDatabase database,
            string              id,
            MongoCollectionName collectionName,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.DeleteDocument(database.GetDocumentCollection<T>(collectionName), id, cancellationToken);

        public static Task<long> DeleteManyDocuments<T>(
            this IMongoDatabase                                   database,
            MongoCollectionName                                   collectionName,
            Func<FilterDefinitionBuilder<T>, FilterDefinition<T>> filter,
            CancellationToken                                     cancellationToken = default
        ) where T : Document
            => database.GetDocumentCollection<T>(collectionName).DeleteManyDocuments(filter, cancellationToken);

        public static Task<long> DeleteManyDocuments<T>(
            this IMongoDatabase database,
            MongoCollectionName collectionName,
            FilterDefinition<T> filter,
            CancellationToken   cancellationToken = default
        ) where T : Document
            => MongoCollectionExtensions.DeleteManyDocuments(database.GetDocumentCollection<T>(collectionName), filter, cancellationToken);

        public static IMongoQueryable<T> AsQueryable<T>(
            this IMongoDatabase      database,
            MongoCollectionName      collectionName,
            Action<AggregateOptions> configure = null
        ) where T : Document {
            var options = new AggregateOptions();
            configure?.Invoke(options);

            return database.GetDocumentCollection<T>(collectionName).AsQueryable(options);
        }

        public static IMongoQueryable<T> AsQueryable<T>(
            this IMongoDatabase      database,
            Action<AggregateOptions> configure = null
        ) where T : Document {
            var options = new AggregateOptions();
            configure?.Invoke(options);

            return MongoCollectionExtensions.GetDocumentCollection<T>(database).AsQueryable(options);
        }

        public static Task<string> CreateDocumentIndex<T>(
            this IMongoDatabase                                         database,
            Func<IndexKeysDefinitionBuilder<T>, IndexKeysDefinition<T>> index,
            Action<CreateIndexOptions>                                  configure = null
        ) where T : Document
            => MongoCollectionExtensions.GetDocumentCollection<T>(database).CreateDocumentIndex(index, configure);
    }
}
