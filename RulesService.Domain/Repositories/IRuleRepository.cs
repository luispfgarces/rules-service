using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Domain.Core;
using RulesService.Domain.Models;

namespace RulesService.Domain.Repositories
{
    public interface IRuleRepository : IRepository<Rule, RuleKey>
    {
        Task<IEnumerable<Rule>> GetAll(Guid tenantId, RulesFilter rulesFilter, Pagination pagination);
    }
}