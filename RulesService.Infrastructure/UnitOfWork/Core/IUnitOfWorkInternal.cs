namespace RulesService.Infrastructure.UnitOfWork.Core
{
    internal interface IUnitOfWorkInternal
    {
        InternalState InternalState { get; }
    }
}