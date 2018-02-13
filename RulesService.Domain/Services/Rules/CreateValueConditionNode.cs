namespace RulesService.Domain.Services.Rules
{
    public class CreateValueConditionNode : CreateConditionNodeBase
    {
        public CreateValueConditionNode()
            : base()
        {
        }

        public int ConditionTypeCode { get; set; }

        public int DataTypeCode { get; set; }

        public int OperatorCode { get; set; }

        public object RightHandOperand { get; set; }
    }
}