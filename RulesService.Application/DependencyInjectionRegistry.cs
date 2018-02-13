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
            serviceCollection.AddScoped<IDataTypeService, DataTypeService>();
            serviceCollection.AddScoped<IContentTypeService, ContentTypeService>();
            serviceCollection.AddScoped<IOperatorService, OperatorService>();
            serviceCollection.AddScoped<IRuleService, RuleService>();

            return serviceCollection;
        }
    }
}