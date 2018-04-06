namespace RulesService.Infrastructure.UnitOfWork.Resources
{
    public interface IResource
    {
        IResourceTransaction BeginTransaction();
    }
}