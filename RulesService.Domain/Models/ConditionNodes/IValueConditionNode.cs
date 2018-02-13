namespace RulesService.Domain.Models.ConditionNodes
{
    public interface IValueConditionNode : IConditionNode
    {
        int ConditionTypeCode { get; }

        DataTypeCodes DataTypeCode { get; }

        OperatorCodes OperatorCode { get; }

        object GetRightHandOperandAsObject();
    }
}