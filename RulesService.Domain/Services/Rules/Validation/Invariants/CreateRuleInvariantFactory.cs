using System;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class CreateRuleInvariantFactory : ICreateRuleInvariantFactory
    {
        private static readonly Type createRuleInvariantInterfaceType = typeof(ICreateRuleInvariant);

        private readonly IServiceProvider serviceProvider;

        public CreateRuleInvariantFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICreateRuleInvariant GetCreateRuleInvariant(Type invariantType)
        {
            if (!CreateRuleInvariantFactory.createRuleInvariantInterfaceType.IsAssignableFrom(invariantType))
            {
                throw new ArgumentException(FormattableString.Invariant($"Provided invariant type is not a {nameof(ICreateRuleInvariant)}."), nameof(invariantType));
            }

            return this.serviceProvider.GetService(invariantType) as ICreateRuleInvariant;
        }
    }
}