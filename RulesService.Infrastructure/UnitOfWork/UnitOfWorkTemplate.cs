using System;
using RulesService.Infrastructure.UnitOfWork.Core;
using RulesService.Infrastructure.UnitOfWork.Core.Messaging;

namespace RulesService.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkTemplate : IUnitOfWork, IUnitOfWorkInternal
    {
        private bool disposedValue;

        private InternalState internalState;

        protected UnitOfWorkTemplate(string name)
        {
            Guid id = Guid.NewGuid();
            this.Code = FormattableString.Invariant($"{name}-{id}");
            this.disposedValue = false;
        }

        public string Code { get; private set; }

        InternalState IUnitOfWorkInternal.InternalState => this.internalState;

        public abstract UnitOfWorkTypeCodes TypeCode { get; }

        public void Complete()
        {
            this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PreDoComplete);
            this.internalState.TransactionalState.SetCompleted();
            this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PostDoComplete);
        }

        public void Dispose()
        {
            if (this.internalState.TransactionalState.Committable)
            {
                this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PreCommit);
                this.Commit();
                this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PostCommit);
            }
            else
            {
                this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PreRollback);
                this.Rollback();
                this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PostRollback);
            }

            this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PreFinalize);
            this.Dispose(true);
            this.DispatchEvent(UnitOfWorkCommandCodes.None, UnitOfWorkStateCodes.PostFinalize);
        }

        internal void InitializeInternalState(InternalState internalState)
        {
            this.internalState = internalState ?? throw new ArgumentNullException(nameof(internalState));
        }

        protected abstract void Commit();

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.internalState = null;
                }

                this.disposedValue = true;
            }
        }

        protected abstract void Rollback();

        private void DispatchEvent(UnitOfWorkCommandCodes unitOfWorkCommand, UnitOfWorkStateCodes unitOfWorkState)
        {
            this.internalState.EventDispatcher.Dispatch(new Core.Messaging.UnitOfWorkEvent
            {
                Code = this.Code,
                Command = unitOfWorkCommand,
                State = unitOfWorkState
            });
        }
    }
}