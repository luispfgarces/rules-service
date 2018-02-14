using System.Linq;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class ChildNodesForComposedConditionNodeCreateRuleInvariant : ComposedConditionNodeCreateRuleInvariantTemplate
    {
        private const string CodeConst = "R006";

        public override string Code => ChildNodesForComposedConditionNodeCreateRuleInvariant.CodeConst;

        protected override string ValidateCurrent(CreateComposedConditionNode createComposedConditionNode)
        {
            if (!createComposedConditionNode.ChildNodes.Any())
            {
                return InvariantResources.R006;
            }

            return null;
        }
    }
}