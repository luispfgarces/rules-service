namespace RulesService.Application.Dto.ConditionTypes
{
    public class ConditionTypeBaseDto
    {
        protected ConditionTypeBaseDto()
        {
        }

        public int DataTypeCode { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}