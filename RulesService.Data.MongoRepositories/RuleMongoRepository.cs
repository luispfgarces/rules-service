using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using RulesService.Data.MongoRepositories.Core;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Data.MongoRepositories
{
    internal class RuleMongoRepository : MongoRepositoryTemplate<Rule, RuleKey>, IRuleRepository
    {
        private const string CollectionNameConst = "Rules";

        public RuleMongoRepository(
            IMongoClientProvider mongoClientProvider,
            IMongoConfiguration mongoConfiguration)
            : base(mongoClientProvider, mongoConfiguration)
        {
        }

        protected override string CollectionName => CollectionNameConst;

        public Task<IEnumerable<Rule>> GetAll(Guid tenantId, RulesFilter rulesFilter, Pagination pagination)
        {
            throw new NotImplementedException();
        }

        protected override FilterDefinition<Rule> GetFilterDefinitionByEntity(Rule entity) => this.GetFilterDefinitionByKey(entity.Key);

        protected override FilterDefinition<Rule> GetFilterDefinitionByKey(RuleKey key) => Builders<Rule>.Filter
            .Eq(r => r.Key, key);

        protected override UpdateDefinition<Rule> GetUpdateDefinitionFor(Rule entity) => Builders<Rule>.Update
            .Set(r => r.AuditMetadata, entity.AuditMetadata)
            .Set(r => r.Content, entity.Content)
            .Set(r => r.ContentTypeCode, entity.ContentTypeCode)
            .Set(r => r.DateBegin, entity.DateBegin)
            .Set(r => r.DateEnd, entity.DateEnd)
            .Set(r => r.Name, entity.Name)
            .Set(r => r.Priority, entity.Priority)
            .Set(r => r.RootCondition, entity.RootCondition);
    }
}