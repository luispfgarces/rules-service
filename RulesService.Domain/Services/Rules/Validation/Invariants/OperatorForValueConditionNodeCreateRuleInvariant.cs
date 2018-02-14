using System;
using RulesService.Domain.Models;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class OperatorForValueConditionNodeCreateRuleInvariant : ValueConditionNodeCreateRuleInvariantTemplate
    {
        private const string CodeConst = "R009";

        public override string Code => OperatorForValueConditionNodeCreateRuleInvariant.CodeConst;

        protected override string ValidateCurrent(CreateValueConditionNode createValueConditionNode, Guid tenantId)
        {
            OperatorCodes operatorCode = (OperatorCodes)createValueConditionNode.OperatorCode;
            if (!Enum.IsDefined(typeof(OperatorCodes), operatorCode))
            {
                return string.Format(InvariantResources.R009, createValueConditionNode.OperatorCode);
            }

            return null;
        }
    }
}