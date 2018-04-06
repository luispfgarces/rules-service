using System;

namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    public interface IEventDispatcher : IObservable<UnitOfWorkEvent>
    {
        void Complete();

        void Dispatch(UnitOfWorkEvent unitOfWorkEvent);
    }
}