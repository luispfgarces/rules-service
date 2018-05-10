using MongoDB.Driver;
using RulesService.Data.MongoRepositories.Core;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Data.MongoRepositories
{
    internal class ContentTypeMongoRepository : MongoRepositoryTemplate<ContentType, ContentTypeKey>, IContentTypeRepository
    {
        private const string CollectionNameConst = "ContentTypes";

        public ContentTypeMongoRepository(
            IMongoClientProvider mongoClientProvider,
            IMongoConfiguration mongoConfiguration)
            : base(mongoClientProvider, mongoConfiguration)
        {
        }

        protected override string CollectionName => CollectionNameConst;

        protected override FilterDefinition<ContentType> GetFilterDefinitionByEntity(ContentType entity) => this.GetFilterDefinitionByKey(entity.Key);

        protected override FilterDefinition<ContentType> GetFilterDefinitionByKey(ContentTypeKey key) => Builders<ContentType>.Filter
            .Eq(t => t.Key, key);

        protected override UpdateDefinition<ContentType> GetUpdateDefinitionFor(ContentType entity) => Builders<ContentType>.Update
            .Set(c => c.AuditMetadata, entity.AuditMetadata)
            .Set(c => c.Name, entity.Name);
    }
}