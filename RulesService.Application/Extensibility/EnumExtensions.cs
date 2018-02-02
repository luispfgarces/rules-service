namespace RulesService.Domain.Models
{
    internal static class EnumExtensions
    {
        public static DataTypeCodes AsDataTypeCode(this int dataTypeCode) => (DataTypeCodes)dataTypeCode;

        public static int AsInteger(this DataTypeCodes dataTypeCode) => (int)dataTypeCode;
    }
}