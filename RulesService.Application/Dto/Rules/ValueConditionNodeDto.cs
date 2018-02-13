namespace RulesService.Application.Dto.Rules
{
    public class ValueConditionNodeDto : ConditionNodeBaseDto
    {
        public int ConditionTypeCode { get; set; }

        public int DataTypeCode { get; set; }

        public int OperatorCode { get; set; }

        public object RightHandOperand { get; set; }
    }
}