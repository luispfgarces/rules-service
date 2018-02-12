using Microsoft.Extensions.DependencyInjection;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;

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
            serviceCollection.AddSingleton<IOperatorRepository, InMemoryCachedOperatorRepository>();

            return serviceCollection;
        }
    }
}