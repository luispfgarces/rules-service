using Microsoft.Extensions.DependencyInjection;
using RulesService.Application.Services;

namespace RulesService.Application
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITenantService, TenantService>();
            serviceCollection.AddScoped<IConditionTypeService, ConditionTypeService>();

            return serviceCollection;
        }
    }
}