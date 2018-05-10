using MongoDB.Driver;

namespace RulesService.Data.MongoRepositories.Core
{
    internal class MongoClientProvider : IMongoClientProvider
    {
        private MongoClient mongoClient;

        public MongoClient GetOrCreate(IMongoConfiguration mongoConfiguration)
        {
            if (this.mongoClient == null)
            {
                this.mongoClient = new MongoClient(mongoConfiguration.ConnectionString);
            }

            return this.mongoClient;
        }
    }
}