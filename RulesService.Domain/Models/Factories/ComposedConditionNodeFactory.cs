using System;
using System.Collections.Generic;
using System.Linq;
using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models.Factories
{
    internal class ComposedConditionNodeFactory : IComposedConditionNodeFactory
    {
        private static readonly LogicalOperatorCodes[] supportedLogicalOperatorCodes = { LogicalOperatorCodes.And, LogicalOperatorCodes.Or };

        public IComposedConditionNode CreateComposedConditionNode(LogicalOperatorCodes logicalOperatorCode, IEnumerable<IConditionNode> childConditions)
        {
            if (!supportedLogicalOperatorCodes.Contains(logicalOperatorCode))
            {
                throw new ArgumentException(
                    "The specified logical operator is invalid for composed condition nodes.",
                    nameof(logicalOperatorCode));
            }

            if (!childConditions.Any())
            {
                throw new ArgumentException(
                    "Provided invalid collection of child condition nodes: must have at least one child condition and provided collection has none.",
                    nameof(childConditions));
            }

            return new ComposedConditionNode(logicalOperatorCode)
            {
                ChildNodes = childConditions
            };
        }
    }
}