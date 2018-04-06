using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal class DirtyArea<T> : IDirtyArea<T>
        where T : class
    {
        private readonly List<Tuple<T, T>> dirtyItems;

        private IDisposable loadedItemObserverUnsubscriber;

        private IDisposable stagedItemObserverUnsubscriber;

        public DirtyArea()
        {
            this.dirtyItems = new List<Tuple<T, T>>(0);
            this.loadedItemObserverUnsubscriber = null;
            this.stagedItemObserverUnsubscriber = null;
        }

        public IEnumerable<T> DirtyItems => this.dirtyItems.Select(i => i.Item1);

        public void OnCompleted()
        {
            this.dirtyItems.Clear();
        }

        public void OnError(Exception error)
        {
            // No errors implemented...
        }

        public void OnNext(LoadedItem<T> value)
        {
            if (!this.dirtyItems.Any(i => object.Equals(i.Item2, value.Persistent)))
            {
                this.dirtyItems.Add(new Tuple<T, T>(value.Loaded, value.Persistent));
            }
        }

        public void OnNext(StagedItem<T> value)
        {
            Tuple<T, T> tuple = this.dirtyItems.SingleOrDefault(i => object.Equals(i.Item2, value.Original));
            if (tuple == default(Tuple<T, T>))
            {
                this.dirtyItems.Add(new Tuple<T, T>(value.Staged, value.Original));
            }
            else
            {
                this.dirtyItems.Remove(tuple);
                if (value.Operation == OperationCodes.Update)
                {
                    tuple = new Tuple<T, T>(value.Staged, value.Original);
                    this.dirtyItems.Add(tuple);
                }
            }
        }

        public void RegisterLoadedArea(ILoadedArea<T> loadedArea)
        {
            if (this.loadedItemObserverUnsubscriber != null)
            {
                this.loadedItemObserverUnsubscriber.Dispose();
                this.loadedItemObserverUnsubscriber = null;
            }

            this.loadedItemObserverUnsubscriber = loadedArea.Subscribe(this);
        }

        public void RegisterStagingArea(IStagingArea<T> stagingArea)
        {
            if (this.stagedItemObserverUnsubscriber != null)
            {
                this.stagedItemObserverUnsubscriber.Dispose();
                this.stagedItemObserverUnsubscriber = null;
            }

            this.stagedItemObserverUnsubscriber = stagingArea.Subscribe(this);
        }
    }
}