using System;

namespace RulesService.Domain.Models.Factories
{
    public interface IRuleFactory
    {
        Rule CreateRule(Guid tenantId, string name, ContentType contentType, DateTime dateBegin, DateTime? dateEnd, int priority, IConditionNode rootCondition);
    }
}