using Microsoft.Extensions.DependencyInjection;
using RulesService.Application.ConversionProfiles;
using RulesService.Application.Services;
using RulesService.Domain;
using RulesService.Domain.Repositories;

namespace RulesService
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

            serviceCollection.AddScoped<ICreateRuleConversionProfile, CreateRuleConversionProfile>();
            serviceCollection.AddScoped<IConditionNodeConversionProfile, ConditionNodeConversionProfile>();
            serviceCollection.AddScoped<IUpdateRuleConversionProfile, UpdateRuleConversionProfile>();

            serviceCollection.AddMongo();
            serviceCollection.AddRepositories(rdc =>
            {
                rdc.ForRepository<IConditionTypeRepository>().UseMongo();
                rdc.ForRepository<IContentTypeRepository>().UseMongo();
                rdc.ForRepository<IOperatorRepository>().UseInMemory();
                rdc.ForRepository<IRuleRepository>().UseMongo();
                rdc.ForRepository<ITenantRepository>().UseMongo();
            });

            return serviceCollection;
        }
    }
}