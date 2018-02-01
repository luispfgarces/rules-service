using Microsoft.Extensions.DependencyInjection;
using RulesService.Application.Services;

namespace RulesService.Application
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITenantService, TenantService>();

            return serviceCollection;
        }
    }
}