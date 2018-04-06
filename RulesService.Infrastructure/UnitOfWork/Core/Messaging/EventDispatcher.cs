using System;
using System.Collections.Generic;

namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly List<IObserver<UnitOfWorkEvent>> observers;

        public EventDispatcher()
        {
            this.observers = new List<IObserver<UnitOfWorkEvent>>(0);
        }

        public void Complete()
        {
            foreach (IObserver<UnitOfWorkEvent> observer in this.observers)
            {
                observer.OnCompleted();
            }
        }

        public void Dispatch(UnitOfWorkEvent unitOfWorkEvent)
        {
            foreach (IObserver<UnitOfWorkEvent> observer in this.observers)
            {
                observer.OnNext(unitOfWorkEvent);
            }
        }

        public IDisposable Subscribe(IObserver<UnitOfWorkEvent> observer)
        {
            if (!this.observers.Contains(observer))
            {
                this.observers.Add(observer);
            }

            return new ObservableUnsubscriber(() =>
            {
                // Store variables inside lambda to avoid marshalling values the outer context (unit of
                // work) when lambda action gets invoked.
                IObserver<UnitOfWorkEvent> localObserver = observer;
                List<IObserver<UnitOfWorkEvent>> localObservers = this.observers;

                localObservers.Remove(localObserver);
            });
        }
    }
}