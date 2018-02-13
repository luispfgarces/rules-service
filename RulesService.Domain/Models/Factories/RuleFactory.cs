using System;

namespace RulesService.Domain.Models.Factories
{
    internal class RuleFactory : IRuleFactory
    {
        public Rule CreateRule(Guid tenantId, string name, ContentType contentType, DateTime dateBegin, DateTime? dateEnd, int priority, IConditionNode rootCondition)
        {
            if (tenantId == default(Guid))
            {
                throw new ArgumentException("Must provide a valid tenant id.", nameof(tenantId));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Must provide a valid name for the rule (not null or empty).", nameof(name));
            }

            if (contentType == null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            if (dateEnd.HasValue && dateEnd.GetValueOrDefault() <= dateBegin)
            {
                throw new ArgumentException("Must provide a date end greater than date begin for the rule.", nameof(dateEnd));
            }

            if (priority <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(priority), priority, "Must provide a priority value for the rule that is greater than zero.");
            }

            return new Rule(tenantId)
            {
                ContentType = contentType,
                DateBegin = dateBegin,
                DateEnd = dateEnd,
                Name = name,
                Priority = priority,
                RootCondition = rootCondition
            };
        }
    }
}