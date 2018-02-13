using System.Collections.Generic;

namespace RulesService.Domain.Models.ConditionNodes
{
    public interface IComposedConditionNode : IConditionNode
    {
        IEnumerable<IConditionNode> ChildNodes { get; }
    }
}