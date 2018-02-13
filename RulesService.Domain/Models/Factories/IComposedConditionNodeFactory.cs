using System.Collections.Generic;
using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models.Factories
{
    internal interface IComposedConditionNodeFactory
    {
        IComposedConditionNode CreateComposedConditionNode(LogicalOperatorCodes logicalOperatorCode, IEnumerable<IConditionNode> childConditions);
    }
}