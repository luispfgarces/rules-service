using Microsoft.Extensions.DependencyInjection;
using RulesService.Domain.Models.Factories;

namespace RulesService.Domain
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITenantFactory, TenantFactory>();
            serviceCollection.AddTransient<IConditionTypeFactory, ConditionTypeFactory>();

            return serviceCollection;
        }
    }
}