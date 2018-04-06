namespace RulesService.Infrastructure.UnitOfWork.Resources
{
    public interface IResourceTransaction
    {
        void Commit();

        void Rollback();
    }
}