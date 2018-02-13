using System.Collections.Generic;
using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models.Factories
{
    internal class ConditionNodeAbstractFactory : IConditionNodeAbstractFactory
    {
        private readonly IComposedConditionNodeFactory composedConditionNodeFactory;

        private readonly IValueConditionNodeFactory valueConditionNodeFactory;

        public ConditionNodeAbstractFactory(
            IComposedConditionNodeFactory composedConditionNodeFactory,
            IValueConditionNodeFactory valueConditionNodeFactory)
        {
            this.composedConditionNodeFactory = composedConditionNodeFactory;
            this.valueConditionNodeFactory = valueConditionNodeFactory;
        }

        public IConditionNode CreateComposedConditionNode(LogicalOperatorCodes logicalOperatorCode, IEnumerable<IConditionNode> childConditions)
        {
            return this.composedConditionNodeFactory.CreateComposedConditionNode(logicalOperatorCode, childConditions);
        }

        public IConditionNode CreateValueConditionNode(ConditionType conditionType, DataTypeCodes dataTypeCode, OperatorCodes operatorCode, object rightHandOperand)
        {
            return this.valueConditionNodeFactory.CreateValueConditionNode(conditionType, dataTypeCode, operatorCode, rightHandOperand);
        }
    }
}