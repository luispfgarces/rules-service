using System;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal interface IInvariantFactory<in T>
    {
        IInvariant<T> GetRuleInvariant(Type invariantType);
    }
}