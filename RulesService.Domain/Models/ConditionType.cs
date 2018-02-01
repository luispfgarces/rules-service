using RulesService.Domain.Core;

namespace RulesService.Domain.Models
{
    public class ConditionType : EntityBase<int>
    {
        public ConditionType(DataTypeCodes dataTypeCode)
        {
            this.DataTypeCode = dataTypeCode;
        }

        public DataTypeCodes DataTypeCode { get; private set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}