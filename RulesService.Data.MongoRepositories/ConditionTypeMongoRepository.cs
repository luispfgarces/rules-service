using MongoDB.Driver;
using RulesService.Data.MongoRepositories.Core;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Data.MongoRepositories
{
    internal class ConditionTypeMongoRepository : MongoRepositoryTemplate<ConditionType, ConditionTypeKey>, IConditionTypeRepository
    {
        private const string CollectionNameConst = "ConditionTypes";

        public ConditionTypeMongoRepository(
            IMongoClientProvider mongoClientProvider,
            IMongoConfiguration mongoConfiguration)
            : base(mongoClientProvider, mongoConfiguration)
        {
        }

        protected override string CollectionName => CollectionNameConst;

        protected override FilterDefinition<ConditionType> GetFilterDefinitionByEntity(ConditionType entity) => this.GetFilterDefinitionByKey(entity.Key);

        protected override FilterDefinition<ConditionType> GetFilterDefinitionByKey(ConditionTypeKey key) => Builders<ConditionType>.Filter
            .Eq(t => t.Key, key);

        protected override UpdateDefinition<ConditionType> GetUpdateDefinitionFor(ConditionType entity) => Builders<ConditionType>.Update
            .Set(c => c.AuditMetadata, entity.AuditMetadata)
            .Set(c => c.DataTypeCode, entity.DataTypeCode)
            .Set(c => c.Description, entity.Description)
            .Set(c => c.Name, entity.Name);
    }
}