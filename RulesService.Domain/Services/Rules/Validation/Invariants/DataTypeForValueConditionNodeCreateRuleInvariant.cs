using System;
using RulesService.Domain.Models;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class DataTypeForValueConditionNodeCreateRuleInvariant : ValueConditionNodeCreateRuleInvariantTemplate
    {
        private const string CodeConst = "R008";

        public override string Code => DataTypeForValueConditionNodeCreateRuleInvariant.CodeConst;

        protected override string ValidateCurrent(CreateValueConditionNode createValueConditionNode, Guid tenantId)
        {
            DataTypeCodes dataTypeCode = (DataTypeCodes)createValueConditionNode.DataTypeCode;
            if (!Enum.IsDefined(typeof(DataTypeCodes), dataTypeCode))
            {
                return string.Format(InvariantResources.R008, createValueConditionNode.DataTypeCode);
            }

            return null;
        }
    }
}