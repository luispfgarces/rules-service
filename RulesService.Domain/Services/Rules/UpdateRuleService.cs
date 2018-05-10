using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;
using RulesService.Domain.Services.Rules.Validation;

namespace RulesService.Domain.Services.Rules
{
    internal class UpdateRuleService : IUpdateRuleService
    {
        private readonly IRuleRepository ruleRepository;
        private readonly IUpdateRuleValidator updateRuleValidator;

        public UpdateRuleService(
            IUpdateRuleValidator updateRuleValidator,
            IRuleRepository ruleRepository)
        {
            this.updateRuleValidator = updateRuleValidator;
            this.ruleRepository = ruleRepository;
        }

        public async Task<RuleResult> UpdateRule(UpdateRule updateRule)
        {
            RuleResult ruleResult = new RuleResult();

            IEnumerable<RuleValidationMessage> ruleValidationMessages = this.updateRuleValidator.Validate(updateRule);

            foreach (RuleValidationMessage validationMessage in ruleValidationMessages)
            {
                ruleResult.AddErrorMessage(validationMessage.Code, validationMessage.Message);
            }

            if (!ruleResult.HasErrors)
            {
                // Fetch rule.
                RuleKey ruleKey = RuleKey.New(updateRule.TenantId, updateRule.Id);
                Rule rule = await this.ruleRepository.GetById(ruleKey);

                // Reorganize rules if rule priority has changed.
                if (rule.Priority != updateRule.Priority)
                {
                    int lowBound;
                    int highBound;
                    bool increasePriorities;
                    if (rule.Priority > updateRule.Priority)
                    {
                        lowBound = updateRule.Priority;
                        highBound = rule.Priority;
                        increasePriorities = true;
                    }
                    else
                    {
                        highBound = updateRule.Priority;
                        lowBound = rule.Priority;
                        increasePriorities = false;
                    }

                    IEnumerable<Rule> existentRules = await this.ruleRepository.GetAll(
                        updateRule.TenantId,
                        new RulesFilter
                        {
                            ContentTypeCode = rule.ContentTypeCode
                        },
                        null);

                    // Move rules according to new rule's priority.
                    foreach (Rule existentRule in this.GetFilteredRulesForPriorityReordering(existentRules, lowBound, highBound, increasePriorities))
                    {
                        if (increasePriorities)
                        {
                            existentRule.Priority++;
                        }
                        else
                        {
                            existentRule.Priority--;
                        }

                        await this.ruleRepository.Update(existentRule);
                    }
                }

                // Update rule with new priority and date end.
                rule.Priority = updateRule.Priority;
                rule.DateEnd = updateRule.DateEnd;

                await this.ruleRepository.Update(rule);

                ruleResult.AffectedRule = rule;
            }

            return ruleResult;
        }

        private IEnumerable<Rule> GetFilteredRulesForPriorityReordering(IEnumerable<Rule> rules, int lowBound, int highBound, bool increasePriorities)
        {
            return increasePriorities
                ? rules.Where(r => r.Priority >= lowBound && r.Priority < highBound)
                : rules.Where(r => r.Priority > lowBound && r.Priority <= highBound);
        }
    }
}