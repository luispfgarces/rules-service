using System;
using RulesService.Domain.Models;
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
                    Id = Guid.Parse("d29c8e8b-0e46-4993-9c77-25e48bdd6691"),
                    Name = "Tenant A"
                },
                new Tenant
                {
                    Id = Guid.Parse("2999babb-16ea-446c-84d5-d08093377e48"),
                    Name = "Tenant B"
                }
            });
        }
    }
}