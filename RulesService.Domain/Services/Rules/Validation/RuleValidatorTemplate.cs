using System;
using System.Collections.Generic;
using RulesService.Domain.Services.Rules.Validation.Invariants;

namespace RulesService.Domain.Services.Rules.Validation
{
    internal abstract class RuleValidatorTemplate<T>
    {
        private readonly IInvariantFactory<T> invariantFactory;

        protected RuleValidatorTemplate(IInvariantFactory<T> invariantFactory)
        {
            this.invariantFactory = invariantFactory;
        }

        protected abstract IEnumerable<Type> InvariantTypes { get; }

        public IEnumerable<RuleValidationMessage> Validate(T rule)
        {
            List<RuleValidationMessage> createRuleValidationMessages = new List<RuleValidationMessage>(0);

            List<IInvariant<T>> createRuleInvariants = new List<IInvariant<T>>(0);
            foreach (Type invariantType in this.InvariantTypes)
            {
                IInvariant<T> createRuleInvariant = this.invariantFactory.GetRuleInvariant(invariantType);
                createRuleInvariants.Add(createRuleInvariant);
            }

            foreach (IInvariant<T> createRuleInvariant in createRuleInvariants)
            {
                InvariantResult invariantResult = createRuleInvariant.IsValid(rule);

                if (!invariantResult.Valid)
                {
                    foreach (string message in invariantResult.Messages)
                    {
                        createRuleValidationMessages.Add(new RuleValidationMessage
                        {
                            Code = invariantResult.Code,
                            Message = message
                        });
                    }
                }
            }

            return createRuleValidationMessages;
        }
    }
}