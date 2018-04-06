namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    internal interface IEventSubscriptionHandlerFactory
    {
        IEventSubscriptionHandler Create(InternalState internalState);
    }
}