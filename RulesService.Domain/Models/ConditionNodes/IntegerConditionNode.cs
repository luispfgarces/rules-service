namespace RulesService.Domain.Models.ConditionNodes
{
    public class IntegerConditionNode : ValueConditionNodeBase<int>
    {
        public IntegerConditionNode(int conditionTypeCode, OperatorCodes operatorCode, int rightHandOperand)
            : base(conditionTypeCode, operatorCode, rightHandOperand)
        {
        }

        public override DataTypeCodes DataTypeCode => DataTypeCodes.Integer;
    }
}