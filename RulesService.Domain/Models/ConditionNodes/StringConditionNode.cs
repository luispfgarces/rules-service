namespace RulesService.Domain.Models.ConditionNodes
{
    public class StringConditionNode : ValueConditionNodeBase<string>
    {
        public StringConditionNode(int conditionTypeCode, OperatorCodes operatorCode, string rightHandOperand)
            : base(conditionTypeCode, operatorCode, rightHandOperand)
        {
        }

        public override DataTypeCodes DataTypeCode => DataTypeCodes.String;
    }
}