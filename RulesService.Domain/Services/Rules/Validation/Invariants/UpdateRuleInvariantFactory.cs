using System;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class UpdateRuleInvariantFactory : IUpdateRuleInvariantFactory
    {
        private static readonly Type updateRuleInvariantInterfaceType = typeof(IUpdateRuleInvariant);

        private readonly IServiceProvider serviceProvider;

        public UpdateRuleInvariantFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IInvariant<UpdateRule> GetRuleInvariant(Type invariantType)
        {
            if (!UpdateRuleInvariantFactory.updateRuleInvariantInterfaceType.IsAssignableFrom(invariantType))
            {
                throw new ArgumentException(FormattableString.Invariant($"Provided invariant type is not a {nameof(IUpdateRuleInvariant)}."), nameof(invariantType));
            }

            return this.serviceProvider.GetService(invariantType) as IUpdateRuleInvariant;
        }
    }
}