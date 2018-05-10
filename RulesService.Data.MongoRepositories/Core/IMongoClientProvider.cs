using MongoDB.Driver;

namespace RulesService.Data.MongoRepositories.Core
{
    internal interface IMongoClientProvider
    {
        MongoClient GetOrCreate(IMongoConfiguration mongoConfiguration);
    }
}