using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models
{
    internal static class EnumExtensions
    {
        public static DataTypeCodes AsDataTypeCode(this int dataTypeCode) => (DataTypeCodes)dataTypeCode;

        public static int AsInteger(this DataTypeCodes dataTypeCode) => (int)dataTypeCode;

        public static int AsInteger(this LogicalOperatorCodes logicalOperatorCode) => (int)logicalOperatorCode;

        public static int AsInteger(this OperatorCodes operatorCode) => (int)operatorCode;
    }
}