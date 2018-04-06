using System;

namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    public interface IEventSubscriptionHandler
    {
        void Subscribe(IObservable<UnitOfWorkEvent> observable);
    }
}