using Microsoft.Extensions.DependencyInjection;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Services.Rules;

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

            return serviceCollection;
        }
    }
}