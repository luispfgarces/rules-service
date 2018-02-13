namespace RulesService.Domain.Models.ConditionNodes
{
    public abstract class ValueConditionNodeBase<T> : IValueConditionNode
    {
        protected ValueConditionNodeBase(int conditionTypeCode, OperatorCodes operatorCode, T rightHandOperand)
        {
            this.ConditionTypeCode = conditionTypeCode;
            this.OperatorCode = operatorCode;
            this.RightHandOperand = rightHandOperand;
        }

        public int ConditionTypeCode { get; private set; }

        public abstract DataTypeCodes DataTypeCode { get; }

        public LogicalOperatorCodes LogicalOperatorCode => LogicalOperatorCodes.Eval;

        public OperatorCodes OperatorCode { get; private set; }

        public T RightHandOperand { get; private set; }

        public object GetRightHandOperandAsObject() => this.RightHandOperand;
    }
}