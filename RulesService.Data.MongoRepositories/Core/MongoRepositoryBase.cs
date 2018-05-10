using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace RulesService.Data.MongoRepositories.Core
{
    internal abstract class MongoRepositoryBase<TEntity>
        where TEntity : class
    {
        protected MongoRepositoryBase(
            IMongoClientProvider mongoClientProvider,
            IMongoConfiguration mongoConfiguration)
        {
            this.MongoConfiguration = mongoConfiguration;
            this.MongoClient = mongoClientProvider.GetOrCreate(this.MongoConfiguration);
            this.MongoDatabase = this.MongoClient.GetDatabase(this.MongoConfiguration.DatabaseName);
            this.MongoCollection = this.GetOrCreateCollection();
        }

        protected abstract string CollectionName { get; }

        protected MongoClient MongoClient { get; private set; }

        protected IMongoCollection<TEntity> MongoCollection { get; private set; }

        protected IMongoConfiguration MongoConfiguration { get; private set; }

        protected IMongoDatabase MongoDatabase { get; private set; }

        private IMongoCollection<TEntity> GetOrCreateCollection()
        {
            IEnumerable<string> collectionNames = this.MongoDatabase.ListCollections().ToEnumerable().Select(d => d["name"].AsString);

            if (!collectionNames.Contains(this.CollectionName))
            {
                this.MongoDatabase.CreateCollection(this.CollectionName);
            }

            return this.MongoDatabase.GetCollection<TEntity>(this.CollectionName);
        }
    }
}