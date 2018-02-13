using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models
{
    public interface IConditionNode
    {
        LogicalOperatorCodes LogicalOperatorCode { get; }
    }
}