﻿using Microsoft.Extensions.DependencyInjection;
using RulesService.Domain.Repositories;

namespace RulesService.Data.InMemoryRepositories
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddInMemoryRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ITenantRepository, TenantInMemoryRepository>();
            serviceCollection.AddSingleton<IConditionTypeRepository, ConditionTypeInMemoryRepository>();
            serviceCollection.AddSingleton<IContentTypeRepository, ContentTypeInMemoryRepository>();
            serviceCollection.AddSingleton<IOperatorRepository, InMemoryCachedOperatorRepository>();

            return serviceCollection;
        }
    }
}