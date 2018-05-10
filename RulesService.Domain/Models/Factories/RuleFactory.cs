using System;

namespace RulesService.Domain.Models.Factories
{
    internal class RuleFactory : IRuleFactory
    {
        public Rule CreateRule(CreateRuleArgs createRuleArgs)
        {
            if (createRuleArgs.TenantId == default(Guid))
            {
                throw new ArgumentException("Must provide a valid tenant id.", nameof(createRuleArgs.TenantId));
            }

            if (string.IsNullOrWhiteSpace(createRuleArgs.Name))
            {
                throw new ArgumentException("Must provide a valid name for the rule (not null or empty).", nameof(createRuleArgs.Name));
            }

            if (string.IsNullOrEmpty(createRuleArgs.Content))
            {
                throw new ArgumentNullException(nameof(createRuleArgs.Content));
            }

            if (createRuleArgs.ContentType == null)
            {
                throw new ArgumentNullException(nameof(createRuleArgs.ContentType));
            }

            if (createRuleArgs.DateEnd.HasValue && createRuleArgs.DateEnd.GetValueOrDefault() <= createRuleArgs.DateBegin)
            {
                throw new ArgumentException("Must provide a date end greater than date begin for the rule.", nameof(createRuleArgs.DateEnd));
            }

            if (createRuleArgs.Priority <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(createRuleArgs.Priority), createRuleArgs.Priority, "Must provide a priority value for the rule that is greater than zero.");
            }

            return new Rule(createRuleArgs.TenantId)
            {
                Content = createRuleArgs.Content,
                ContentTypeCode = createRuleArgs.ContentType.Key.Code,
                DateBegin = createRuleArgs.DateBegin,
                DateEnd = createRuleArgs.DateEnd,
                Name = createRuleArgs.Name,
                Priority = createRuleArgs.Priority,
                RootCondition = createRuleArgs.RootCondition
            };
        }
    }
}