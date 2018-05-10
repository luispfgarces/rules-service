using Microsoft.Extensions.DependencyInjection;
using RulesService.Data.MongoRepositories.Core;

namespace RulesService
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddMongo(this IServiceCollection serviceCollection)
        {
            // Mongo client provider - must be singleton as it should be only constructed once for a
            // application. Creation cost is heavy and multiple creations are error-prone.
            serviceCollection.AddSingleton<IMongoClientProvider, MongoClientProvider>();

            // Configuration
            serviceCollection.AddSingleton<IMongoConfiguration, MongoConfiguration>();

            return serviceCollection;
        }
    }
}