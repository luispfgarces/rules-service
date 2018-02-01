using Microsoft.Extensions.DependencyInjection;

namespace RulesService.Domain
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddSingleton<ITenantRepository, MockTenantRepository>();

            return serviceCollection;
        }
    }
}