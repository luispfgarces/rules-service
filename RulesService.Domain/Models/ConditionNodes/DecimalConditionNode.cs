namespace RulesService.Domain.Models.ConditionNodes
{
    public class DecimalConditionNode : ValueConditionNodeBase<decimal>
    {
        public DecimalConditionNode(OperatorCodes operatorCode, decimal rightHandOperand)
            : base(operatorCode, rightHandOperand)
        {
        }

        public override DataTypeCodes DataTypeCode => DataTypeCodes.Decimal;
    }
}