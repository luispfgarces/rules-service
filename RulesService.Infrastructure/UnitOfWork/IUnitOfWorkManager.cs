namespace RulesService.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork BeginUnitOfWork();

        IUnitOfWork BeginUnitOfWork(string name);
    }
}