using System;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal interface ICreateRuleInvariantFactory
    {
        ICreateRuleInvariant GetCreateRuleInvariant(Type invariantType);
    }
}