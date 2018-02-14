using System;
using System.Collections.Generic;
using RulesService.Domain.Services.Rules.Validation.Invariants;

namespace RulesService.Domain.Services.Rules.Validation
{
    internal class CreateRuleValidator : ICreateRuleValidator
    {
        private static readonly IEnumerable<Type> invariantTypes = new[]
        {
            typeof(ChildNodesForComposedConditionNodeCreateRuleInvariant),
            typeof(ConditionTypeForValueConditionNodeCreateRuleInvariant),
            typeof(ContentTypeCreateRuleInvariant),
            typeof(DataTypeForValueConditionNodeCreateRuleInvariant),
            typeof(DateIntervalCreateRuleInvariant),
            typeof(NameCreateRuleInvariant),
            typeof(OperatorForValueConditionNodeCreateRuleInvariant),
            typeof(PriorityCreateRuleInvariant),
            typeof(ValidLogicalOperatorForComposedConditionNodeCreateRuleInvariant),
            typeof(ValidValueForValueConditionNodeCreateRuleInvariant)
        };

        private readonly ICreateRuleInvariantFactory createRuleInvariantFactory;

        public CreateRuleValidator(ICreateRuleInvariantFactory createRuleInvariantFactory)
        {
            this.createRuleInvariantFactory = createRuleInvariantFactory;
        }

        public IEnumerable<CreateRuleValidationMessage> Validate(CreateRule createRule)
        {
            List<CreateRuleValidationMessage> createRuleValidationMessages = new List<CreateRuleValidationMessage>(0);

            List<ICreateRuleInvariant> createRuleInvariants = new List<ICreateRuleInvariant>(0);
            foreach (Type invariantType in CreateRuleValidator.invariantTypes)
            {
                ICreateRuleInvariant createRuleInvariant = this.createRuleInvariantFactory.GetCreateRuleInvariant(invariantType);
                createRuleInvariants.Add(createRuleInvariant);
            }

            foreach (ICreateRuleInvariant createRuleInvariant in createRuleInvariants)
            {
                InvariantResult invariantResult = createRuleInvariant.IsValid(createRule);

                if (!invariantResult.Valid)
                {
                    foreach (string message in invariantResult.Messages)
                    {
                        createRuleValidationMessages.Add(new CreateRuleValidationMessage
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