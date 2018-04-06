namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal interface IDeepCopyService
    {
        object Copy(object obj);
    }
}