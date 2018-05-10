using System;
using Microsoft.Extensions.DependencyInjection;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;
using RulesService.Domain.Services.Rules;
using RulesService.Domain.Services.Rules.Validation;
using RulesService.Domain.Services.Rules.Validation.Invariants;

namespace RulesService
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
            serviceCollection.AddTransient<IUpdateRuleService, UpdateRuleService>();
            serviceCollection.AddSingleton<IUpdateRuleInvariantFactory, UpdateRuleInvariantFactory>();
            serviceCollection.AddTransient<IUpdateRuleValidator, UpdateRuleValidator>();
            serviceCollection.AddInvariants();

            return serviceCollection;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection serviceCollection,
            Action<RepositoriesDependencyConfiguration> repositoriesDependencyConfigurationAction)
        {
            RepositoriesDependencyConfiguration repositoriesDependencyConfiguration = new RepositoriesDependencyConfiguration(serviceCollection);

            repositoriesDependencyConfigurationAction.Invoke(repositoriesDependencyConfiguration);

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
            serviceCollection.AddTransient<DateIntervalUpdateRuleInvariant>();
            serviceCollection.AddTransient<PriorityUpdateRuleInvariant>();
        }
    }
}