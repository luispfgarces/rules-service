namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    internal interface IEventDispatcherFactory
    {
        IEventDispatcher Create();
    }
}