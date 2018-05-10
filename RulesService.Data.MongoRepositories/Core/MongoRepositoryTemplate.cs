using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using RulesService.Domain.Core;

namespace RulesService.Data.MongoRepositories.Core
{
    internal abstract class MongoRepositoryTemplate<TEntity, TKey> : MongoRepositoryBase<TEntity>, IRepository<TEntity, TKey>
        where TEntity : EntityBase<TEntity, TKey>
        where TKey : new()
    {
        protected MongoRepositoryTemplate(
            IMongoClientProvider mongoClientProvider,
            IMongoConfiguration mongoConfiguration)
            : base(mongoClientProvider, mongoConfiguration)
        {
        }

        public async Task Add(TEntity entity)
        {
            await this.MongoCollection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Empty;

            IAsyncCursor<TEntity> cursor = await this.MongoCollection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            IAsyncCursor<TEntity> contentTypesCursor = await this.MongoCollection.FindAsync(this.GetFilterDefinitionByKey(id));

            return await contentTypesCursor.FirstOrDefaultAsync();
        }

        public async Task Remove(TEntity entity)
        {
            await this.MongoCollection.DeleteOneAsync(this.GetFilterDefinitionByEntity(entity));
        }

        public async Task Update(TEntity entity)
        {
            entity.AuditMetadata.StampAsModified(DateTime.UtcNow);

            await this.MongoCollection.UpdateOneAsync(
                this.GetFilterDefinitionByEntity(entity),
                this.GetUpdateDefinitionFor(entity));
        }

        protected abstract FilterDefinition<TEntity> GetFilterDefinitionByEntity(TEntity entity);

        protected abstract FilterDefinition<TEntity> GetFilterDefinitionByKey(TKey key);

        protected abstract UpdateDefinition<TEntity> GetUpdateDefinitionFor(TEntity entity);
    }
}