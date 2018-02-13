using System.Collections.Generic;

namespace RulesService.Domain.Models.ConditionNodes
{
    public class ComposedConditionNode : IConditionNode
    {
        private readonly LogicalOperatorCodes logicalOperatorCode;

        public ComposedConditionNode(LogicalOperatorCodes logicalOperatorCode)
        {
            this.logicalOperatorCode = logicalOperatorCode;
        }

        public IEnumerable<IConditionNode> ChildNodes { get; set; }

        public LogicalOperatorCodes LogicalOperatorCode => this.logicalOperatorCode;
    }
}