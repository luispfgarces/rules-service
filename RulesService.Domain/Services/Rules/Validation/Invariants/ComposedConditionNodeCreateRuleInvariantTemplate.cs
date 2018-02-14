using System.Collections.Generic;
using System.Linq;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal abstract class ComposedConditionNodeCreateRuleInvariantTemplate : ICreateRuleInvariant
    {
        public abstract string Code { get; }

        public InvariantResult IsValid(CreateRule obj)
        {
            if (obj.RootCondition != null)
            {
                IEnumerable<string> messages = this.ValidateRecursive(obj.RootCondition);

                if (messages.Any())
                {
                    return InvariantResult.ForInvalid(this.Code, messages.Distinct().ToArray());
                }
            }

            return InvariantResult.ForValid(this.Code);
        }

        protected abstract string ValidateCurrent(CreateComposedConditionNode createComposedConditionNode);

        private IEnumerable<string> ValidateRecursive(CreateConditionNodeBase createConditionNodeBase)
        {
            List<string> messages = new List<string>(0);

            if (createConditionNodeBase is CreateComposedConditionNode createComposedConditionNode)
            {
                foreach (CreateConditionNodeBase child in createComposedConditionNode.ChildNodes)
                {
                    IEnumerable<string> childMessages = this.ValidateRecursive(child);
                    messages.AddRange(childMessages);
                }

                string message = this.ValidateCurrent(createComposedConditionNode);

                if (message != null)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }
    }
}