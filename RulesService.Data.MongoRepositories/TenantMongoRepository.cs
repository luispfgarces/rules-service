using System;
using MongoDB.Driver;
using RulesService.Data.MongoRepositories.Core;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Data.MongoRepositories
{
    internal class TenantMongoRepository : MongoRepositoryTemplate<Tenant, Guid>, ITenantRepository
    {
        private const string CollectionNameConst = "Tenants";

        public TenantMongoRepository(
            IMongoClientProvider mongoClientProvider,
            IMongoConfiguration mongoConfiguration)
            : base(mongoClientProvider, mongoConfiguration)
        {
        }

        protected override string CollectionName => CollectionNameConst;

        protected override FilterDefinition<Tenant> GetFilterDefinitionByEntity(Tenant entity) => this.GetFilterDefinitionByKey(entity.Id);

        protected override FilterDefinition<Tenant> GetFilterDefinitionByKey(Guid key) => Builders<Tenant>.Filter
            .Eq(t => t.Id, key);

        protected override UpdateDefinition<Tenant> GetUpdateDefinitionFor(Tenant entity) => Builders<Tenant>.Update
            .Set(c => c.AuditMetadata, entity.AuditMetadata)
            .Set(c => c.Name, entity.Name);
    }
}