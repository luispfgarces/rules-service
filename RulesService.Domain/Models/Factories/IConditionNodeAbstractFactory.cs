using System.Collections.Generic;
using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models.Factories
{
    public interface IConditionNodeAbstractFactory
    {
        IConditionNode CreateComposedConditionNode(LogicalOperatorCodes logicalOperatorCode, IEnumerable<IConditionNode> childConditions);

        IConditionNode CreateValueConditionNode(ConditionType conditionType, DataTypeCodes dataTypeCode, OperatorCodes operatorCode, object rightHandOperand);
    }
}