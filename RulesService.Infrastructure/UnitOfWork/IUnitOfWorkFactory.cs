namespace RulesService.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IMasterUnitOfWork CreateMaster(string name);

        ISlaveUnitOfWork CreateSlave(string name);
    }
}