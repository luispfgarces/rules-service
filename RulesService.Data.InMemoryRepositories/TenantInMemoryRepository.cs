using System;
using RulesService.Domain.Model;
using RulesService.Domain.Repositories;

namespace RulesService.Data.InMemoryRepositories
{
    internal class TenantInMemoryRepository : InMemoryRepositoryBase<Tenant, Guid>, ITenantRepository
    {
        public TenantInMemoryRepository()
            : base()
        {
            this.entities.AddRange(new[]
            {
                new Tenant
                {
                    Name = "Tenant A"
                },
                new Tenant
                {
                    Name = "Tenant B"
                }
            });
        }
    }
}