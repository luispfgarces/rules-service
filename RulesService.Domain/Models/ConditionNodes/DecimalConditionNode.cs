namespace RulesService.Domain.Models.ConditionNodes
{
    public class DecimalConditionNode : ValueConditionNodeBase<decimal>
    {
        public DecimalConditionNode(int conditionTypeCode, OperatorCodes operatorCode, decimal rightHandOperand)
            : base(conditionTypeCode, operatorCode, rightHandOperand)
        {
        }

        public override DataTypeCodes DataTypeCode => DataTypeCodes.Decimal;
    }
}