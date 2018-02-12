using RulesService.Domain.Models.Operators;

namespace RulesService.Domain.Models
{
    public enum OperatorCodes
    {
        [OperatorDescription("Equal operator"), OperatorSymbols("=")]
        Equal = 1,

        [OperatorDescription("Not Equal operator"), OperatorSymbols("<>")]
        NotEqual = 2,

        [OperatorDescription("Greater than operator"), OperatorSymbols(">")]
        GreaterThan = 3,

        [OperatorDescription("Greater than or equal operator"), OperatorSymbols(">=")]
        GreaterThanOrEqual = 4,

        [OperatorDescription("Lesser than operator"), OperatorSymbols("<")]
        LesserThan = 5,

        [OperatorDescription("Lesser than or equal operator"), OperatorSymbols("<=")]
        LesserThanOrEqual = 6
    }
}