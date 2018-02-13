namespace RulesService.Domain.Models.ConditionNodes
{
    public class StringConditionNode : ValueConditionNodeBase<string>
    {
        public StringConditionNode(OperatorCodes operatorCode, string rightHandOperand)
            : base(operatorCode, rightHandOperand)
        {
        }

        public override DataTypeCodes DataTypeCode => DataTypeCodes.String;
    }
}