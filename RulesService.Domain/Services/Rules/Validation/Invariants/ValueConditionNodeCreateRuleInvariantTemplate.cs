using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal abstract class ValueConditionNodeCreateRuleInvariantTemplate : ICreateRuleInvariant
    {
        public abstract string Code { get; }

        public InvariantResult IsValid(CreateRule obj)
        {
            if (obj.RootCondition != null)
            {
                IEnumerable<string> messages = this.ValidateRecursive(obj.RootCondition, obj.TenantId);

                if (messages.Any())
                {
                    return InvariantResult.ForInvalid(this.Code, messages.Distinct().ToArray());
                }
            }

            return InvariantResult.ForValid(this.Code);
        }

        protected abstract string ValidateCurrent(CreateValueConditionNode createValueConditionNode, Guid tenantId);

        private IEnumerable<string> ValidateRecursive(CreateConditionNodeBase createConditionNodeBase, Guid tenantId)
        {
            List<string> messages = new List<string>(0);

            switch (createConditionNodeBase)
            {
                case CreateComposedConditionNode createComposedConditionNode:
                    foreach (CreateConditionNodeBase child in createComposedConditionNode.ChildNodes)
                    {
                        IEnumerable<string> childMessages = this.ValidateRecursive(child, tenantId);
                        messages.AddRange(childMessages);
                    }

                    break;

                case CreateValueConditionNode createValueConditionNode:
                    string message = this.ValidateCurrent(createValueConditionNode, tenantId);

                    if (message != null)
                    {
                        messages.Add(message);
                    }

                    break;

                default:
                    break;
            }

            return messages;
        }
    }
}