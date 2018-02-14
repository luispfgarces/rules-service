namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal interface IInvariant<in T>
    {
        string Code { get; }

        InvariantResult IsValid(T obj);
    }
}