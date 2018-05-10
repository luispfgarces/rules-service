namespace RulesService.Data.MongoRepositories.Core
{
    internal interface IMongoConfiguration
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}