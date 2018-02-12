using RulesService.Domain.Models.Operators;

namespace RulesService.Domain.Models.Factories
{
    public interface IOperatorFactory
    {
        Operator CreateOperator(OperatorCodes operatorCode);
    }
}