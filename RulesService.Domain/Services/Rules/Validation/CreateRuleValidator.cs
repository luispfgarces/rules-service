using System;
using System.Collections.Generic;
using RulesService.Domain.Services.Rules.Validation.Invariants;

namespace RulesService.Domain.Services.Rules.Validation
{
    internal class CreateRuleValidator : RuleValidatorTemplate<CreateRule>, ICreateRuleValidator
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

        public CreateRuleValidator(ICreateRuleInvariantFactory createRuleInvariantFactory)
            : base(createRuleInvariantFactory)
        {
        }

        protected override IEnumerable<Type> InvariantTypes => CreateRuleValidator.invariantTypes;
    }
}