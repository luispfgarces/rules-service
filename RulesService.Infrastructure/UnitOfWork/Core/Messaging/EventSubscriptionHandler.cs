using System;
using System.Collections.Generic;

namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    internal class EventSubscriptionHandler : IEventSubscriptionHandler
    {
        private readonly List<UnitOfWorkEventObserver> handlers;

        private readonly InternalState internalState;

        public EventSubscriptionHandler(InternalState internalState)
        {
            this.handlers = new List<UnitOfWorkEventObserver>(0);
            this.internalState = internalState;
        }

        public void Subscribe(IObservable<UnitOfWorkEvent> observable)
        {
            UnitOfWorkEventObserver unitOfWorkEventObserver = new UnitOfWorkEventObserver((o) =>
            {
                List<UnitOfWorkEventObserver> localObservers = this.handlers;

                localObservers.Remove(o);
            }, this.internalState);

            this.handlers.Add(unitOfWorkEventObserver);

            unitOfWorkEventObserver.Subscribe(observable);
        }
    }
}