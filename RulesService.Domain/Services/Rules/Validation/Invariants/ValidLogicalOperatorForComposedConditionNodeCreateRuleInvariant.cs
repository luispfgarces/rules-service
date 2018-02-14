using System.Linq;
using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class ValidLogicalOperatorForComposedConditionNodeCreateRuleInvariant : ComposedConditionNodeCreateRuleInvariantTemplate
    {
        private const string CodeConst = "R005";

        private static readonly LogicalOperatorCodes[] allowedLogicalOperatorCodes = { LogicalOperatorCodes.And, LogicalOperatorCodes.Or };

        public override string Code => ValidLogicalOperatorForComposedConditionNodeCreateRuleInvariant.CodeConst;

        protected override string ValidateCurrent(CreateComposedConditionNode createComposedConditionNode)
        {
            LogicalOperatorCodes logicalOperatorCode = (LogicalOperatorCodes)createComposedConditionNode.LogicalOperatorCode;

            if (!ValidLogicalOperatorForComposedConditionNodeCreateRuleInvariant.allowedLogicalOperatorCodes.Contains(logicalOperatorCode))
            {
                return string.Format(InvariantResources.R005, logicalOperatorCode);
            }

            return null;
        }
    }
}