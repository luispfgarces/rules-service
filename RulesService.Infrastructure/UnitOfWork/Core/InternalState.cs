using RulesService.Infrastructure.UnitOfWork.Core.Messaging;

namespace RulesService.Infrastructure.UnitOfWork.Core
{
    internal class InternalState
    {
        public InternalState(
            IEventDispatcherFactory eventDispatcherFactory,
            IEventSubscriptionHandlerFactory eventSubscriptionHandlerFactory)
        {
            this.EventDispatcher = eventDispatcherFactory.Create();
            this.EventSubscriptionHandler = eventSubscriptionHandlerFactory.Create(this);
            this.TransactionalState = new TransactionalState();
        }

        public IEventDispatcher EventDispatcher { get; }

        public IEventSubscriptionHandler EventSubscriptionHandler { get; }

        public TransactionalState TransactionalState { get; private set; }
    }
}