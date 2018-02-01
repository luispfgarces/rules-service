using System;
using RulesService.Domain.Core;
using RulesService.Domain.Model;

namespace RulesService.Domain.Repositories
{
    public interface ITenantRepository : IRepository<Tenant, Guid>
    {
    }
}