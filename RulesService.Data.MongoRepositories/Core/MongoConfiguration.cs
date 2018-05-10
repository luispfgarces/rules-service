using Microsoft.Extensions.Configuration;

namespace RulesService.Data.MongoRepositories.Core
{
    internal class MongoConfiguration : IMongoConfiguration
    {
        private readonly IConfiguration configuration;

        public MongoConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ConnectionString => this.FetchConfiguration("Mongo:ConnectionString");

        public string DatabaseName => this.FetchConfiguration("Mongo:DatabaseName");

        private string FetchConfiguration(string key)
        {
            return this.configuration[key];
        }
    }
}