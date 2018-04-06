using System;

namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    internal class ObservableUnsubscriber : IDisposable
    {
        private readonly Action unsubscribeAction;

        private bool disposedValue = false;

        public ObservableUnsubscriber(Action unsubscribeAction)
        {
            this.unsubscribeAction = unsubscribeAction ?? throw new ArgumentNullException(nameof(unsubscribeAction));
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.unsubscribeAction.Invoke();
                }

                this.disposedValue = true;
            }
        }
    }
}