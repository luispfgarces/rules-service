using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models.Factories
{
    internal interface IValueConditionNodeFactory
    {
        IValueConditionNode CreateValueConditionNode(ConditionType conditionType, DataTypeCodes dataTypeCode, OperatorCodes operatorCode, object rightHandOperand);
    }
}