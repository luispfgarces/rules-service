using System;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class ConditionTypeForValueConditionNodeCreateRuleInvariant : ValueConditionNodeCreateRuleInvariantTemplate
    {
        private const string CodeConst = "R007";

        private readonly IConditionTypeRepository conditionTypeRepository;

        public ConditionTypeForValueConditionNodeCreateRuleInvariant(IConditionTypeRepository conditionTypeRepository)
        {
            this.conditionTypeRepository = conditionTypeRepository;
        }

        public override string Code => ConditionTypeForValueConditionNodeCreateRuleInvariant.CodeConst;

        protected override string ValidateCurrent(CreateValueConditionNode createValueConditionNode, Guid tenantId)
        {
            ConditionTypeKey conditionTypeKey = ConditionTypeKey.New(tenantId, createValueConditionNode.ConditionTypeCode);
            ConditionType conditionType = this.conditionTypeRepository.GetById(conditionTypeKey).GetAwaiter().GetResult();

            if (conditionType == null)
            {
                return string.Format(InvariantResources.R007, tenantId, createValueConditionNode.ConditionTypeCode);
            }

            return null;
        }
    }
}