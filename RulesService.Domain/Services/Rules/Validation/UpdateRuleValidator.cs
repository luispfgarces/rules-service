using System;
using System.Collections.Generic;
using RulesService.Domain.Services.Rules.Validation.Invariants;

namespace RulesService.Domain.Services.Rules.Validation
{
    internal class UpdateRuleValidator : RuleValidatorTemplate<UpdateRule>, IUpdateRuleValidator
    {
        private static readonly IEnumerable<Type> invariantTypes = new[]
        {
            typeof(PriorityUpdateRuleInvariant),
            typeof(DateIntervalUpdateRuleInvariant)
        };

        public UpdateRuleValidator(IUpdateRuleInvariantFactory updateRuleInvariantFactory)
            : base(updateRuleInvariantFactory)
        {
        }

        protected override IEnumerable<Type> InvariantTypes => UpdateRuleValidator.invariantTypes;
    }
}