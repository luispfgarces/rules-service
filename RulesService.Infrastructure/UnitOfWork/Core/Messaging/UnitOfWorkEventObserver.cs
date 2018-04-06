using System;

namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    internal class UnitOfWorkEventObserver : IObserver<UnitOfWorkEvent>
    {
        private bool completed;
        private IDisposable disposable;
        private Action<UnitOfWorkEventObserver> handlerUnregisteringAction;
        private InternalState internalState;

        public UnitOfWorkEventObserver(
            Action<UnitOfWorkEventObserver> handlerUnregisteringAction,
            InternalState internalState)
        {
            this.completed = false;
            this.handlerUnregisteringAction = handlerUnregisteringAction;
            this.internalState = internalState;
        }

        public void OnCompleted()
        {
            if (!this.completed)
            {
                // Finalization logic.
                this.disposable.Dispose();
                this.handlerUnregisteringAction.Invoke(this);

                // Release objects to garbage collector.
                this.disposable = null;
                this.handlerUnregisteringAction = null;
                this.internalState = null;

                // Mark as disposed to prevent redundant calls.
                this.completed = true;
            }
        }

        public void OnError(Exception error)
        {
            // Nothing to do for now...
        }

        public void OnNext(UnitOfWorkEvent value)
        {
            switch (value.Command)
            {
                case UnitOfWorkCommandCodes.None:
                    break;

                case UnitOfWorkCommandCodes.VoteNok:
                    this.internalState.TransactionalState.Vote(value.Code, false);
                    break;

                case UnitOfWorkCommandCodes.VoteOk:
                    this.internalState.TransactionalState.Vote(value.Code, true);
                    break;

                default:
                    break;
            }

            switch (value.State)
            {
                case UnitOfWorkStateCodes.PreBegin:
                    break;

                case UnitOfWorkStateCodes.PostBegin:
                    break;

                case UnitOfWorkStateCodes.PreRollback:
                    break;

                case UnitOfWorkStateCodes.PostRollback:
                    break;

                case UnitOfWorkStateCodes.PreDoComplete:
                    break;

                case UnitOfWorkStateCodes.PostDoComplete:
                    break;

                case UnitOfWorkStateCodes.PreFinalize:
                    break;

                case UnitOfWorkStateCodes.PostFinalize:
                    break;

                default:
                    break;
            }
        }

        public void Subscribe(IObservable<UnitOfWorkEvent> observable)
        {
            this.disposable = observable.Subscribe(this);
        }
    }
}