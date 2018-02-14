using Microsoft.Extensions.DependencyInjection;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Services.Rules;
using RulesService.Domain.Services.Rules.Validation;
using RulesService.Domain.Services.Rules.Validation.Invariants;

namespace RulesService.Domain
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITenantFactory, TenantFactory>();
            serviceCollection.AddTransient<IConditionTypeFactory, ConditionTypeFactory>();
            serviceCollection.AddTransient<IContentTypeFactory, ContentTypeFactory>();
            serviceCollection.AddTransient<IOperatorFactory, OperatorFactory>();
            serviceCollection.AddTransient<IRuleFactory, RuleFactory>();
            serviceCollection.AddTransient<IConditionNodeAbstractFactory, ConditionNodeAbstractFactory>();
            serviceCollection.AddTransient<IComposedConditionNodeFactory, ComposedConditionNodeFactory>();
            serviceCollection.AddTransient<IValueConditionNodeFactory, ValueConditionNodeFactory>();

            serviceCollection.AddTransient<ICreateRuleService, CreateRuleService>();
            serviceCollection.AddSingleton<ICreateRuleInvariantFactory, CreateRuleInvariantFactory>();
            serviceCollection.AddTransient<ICreateRuleValidator, CreateRuleValidator>();
            serviceCollection.AddInvariants();

            return serviceCollection;
        }

        private static void AddInvariants(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ChildNodesForComposedConditionNodeCreateRuleInvariant>();
            serviceCollection.AddTransient<ConditionTypeForValueConditionNodeCreateRuleInvariant>();
            serviceCollection.AddTransient<ContentTypeCreateRuleInvariant>();
            serviceCollection.AddTransient<DataTypeForValueConditionNodeCreateRuleInvariant>();
            serviceCollection.AddTransient<DateIntervalCreateRuleInvariant>();
            serviceCollection.AddTransient<NameCreateRuleInvariant>();
            serviceCollection.AddTransient<OperatorForValueConditionNodeCreateRuleInvariant>();
            serviceCollection.AddTransient<PriorityCreateRuleInvariant>();
            serviceCollection.AddTransient<ValidLogicalOperatorForComposedConditionNodeCreateRuleInvariant>();
            serviceCollection.AddTransient<ValidValueForValueConditionNodeCreateRuleInvariant>();
        }
    }
}