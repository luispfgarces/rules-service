namespace RulesService.Domain.Models.ConditionNodes
{
    public class IntegerConditionNode : ValueConditionNodeBase<int>
    {
        public IntegerConditionNode(OperatorCodes operatorCode, int rightHandOperand)
            : base(operatorCode, rightHandOperand)
        {
        }

        public override DataTypeCodes DataTypeCode => DataTypeCodes.Integer;
    }
}