using System;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class DateIntervalUpdateRuleInvariant : IUpdateRuleInvariant
    {
        private const string CodeConst = "R002";

        private readonly IRuleRepository ruleRepository;

        public DateIntervalUpdateRuleInvariant(IRuleRepository ruleRepository)
        {
            this.ruleRepository = ruleRepository;
        }

        public string Code => DateIntervalUpdateRuleInvariant.CodeConst;

        public InvariantResult IsValid(UpdateRule obj)
        {
            // Validate rule interval.
            if (obj.DateEnd.HasValue)
            {
                RuleKey ruleKey = RuleKey.New(obj.TenantId, obj.Id);
                Rule rule = this.ruleRepository.GetById(ruleKey).GetAwaiter().GetResult();

                if (rule != null)
                {
                    DateTime dateBegin = rule.DateBegin;
                    DateTime dateEnd = obj.DateEnd.GetValueOrDefault();
                    if (dateEnd <= dateBegin)
                    {
                        return InvariantResult.ForInvalid(this.Code, string.Format(InvariantResources.R002, dateBegin, dateEnd));
                    }
                }
            }

            return InvariantResult.ForValid(this.Code);
        }
    }
}