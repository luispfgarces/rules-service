using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Domain.Models;
using RulesService.Domain.Models.ConditionNodes;
using RulesService.Domain.Repositories;

namespace RulesService.Data.InMemoryRepositories
{
    internal class RuleInMemoryRepository : InMemoryRepositoryBase<Rule, RuleKey>, IRuleRepository
    {
        private readonly IContentTypeRepository contentTypeRepository;

        public RuleInMemoryRepository(IContentTypeRepository contentTypeRepository)
        {
            this.contentTypeRepository = contentTypeRepository;
            this.entities.Add(new Rule(Guid.Parse("d29c8e8b-0e46-4993-9c77-25e48bdd6691"))
            {
                ContentType = this.contentTypeRepository.GetById(ContentTypeKey.New(Guid.Parse("d29c8e8b-0e46-4993-9c77-25e48bdd6691"), 1)).GetAwaiter().GetResult(),
                DateBegin = new DateTime(2017, 01, 01),
                Name = "Sample rule",
                Priority = 1,
                RootCondition = new ComposedConditionNode(LogicalOperatorCodes.And)
                {
                    ChildNodes = new IConditionNode[]
                    {
                        new DecimalConditionNode(OperatorCodes.Equal, 23.0m),
                        new IntegerConditionNode(OperatorCodes.GreaterThan, 15)
                    }
                }
            });
        }

        public Task<IEnumerable<Rule>> GetAll(Guid tenantId, RulesFilter rulesFilter, Pagination pagination)
        {
            IEnumerable<Rule> filteredRules = this.entities.Where(r => r.Key.TenantId == tenantId);

            if (rulesFilter != null)
            {
                if (rulesFilter.ContentTypeCode.HasValue)
                {
                    filteredRules = filteredRules.Where(r => r.ContentType.Key.Code == rulesFilter.ContentTypeCode.GetValueOrDefault());
                }

                if (rulesFilter.FilterDateBegin.HasValue && rulesFilter.FilterDateEnd.HasValue)
                {
                    filteredRules = filteredRules.Where(r =>
                        (r.DateBegin >= rulesFilter.FilterDateBegin.GetValueOrDefault() || r.DateEnd.GetValueOrDefault(DateTime.MaxValue) >= rulesFilter.FilterDateBegin.GetValueOrDefault())
                        && (r.DateBegin < rulesFilter.FilterDateEnd.GetValueOrDefault() || r.DateEnd.GetValueOrDefault(DateTime.MaxValue) < rulesFilter.FilterDateEnd.GetValueOrDefault()));
                }
            }

            if (pagination != null)
            {
                filteredRules = filteredRules.Skip(pagination.PageSize * pagination.PageIndex).Take(pagination.PageSize);
            }

            return Task.FromResult(filteredRules);
        }
    }
}