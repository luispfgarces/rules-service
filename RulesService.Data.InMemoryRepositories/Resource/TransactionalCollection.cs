using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal class TransactionalCollection<T> : ITransactionalCollection<T>
        where T : class
    {
        private readonly IDirtyArea<T> dirtyArea;
        private readonly ILoadedArea<T> loadedArea;
        private readonly List<T> persistentArea;
        private readonly IStagingArea<T> stagingArea;

        public TransactionalCollection()
        {
            this.persistentArea = new List<T>(0);
            this.dirtyArea = new DirtyArea<T>();
            this.loadedArea = new LoadedArea<T>(null);
            this.stagingArea = new StagingArea<T>(null);
            this.SyncRoot = new object();

            this.dirtyArea.RegisterLoadedArea(this.loadedArea);
            this.dirtyArea.RegisterStagingArea(this.stagingArea);
        }

        public int Count => this.dirtyArea.DirtyItems.Count();

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot { get; private set; }

        public void Add(T item)
        {
            this.stagingArea.Stage(null, item, OperationCodes.Add);
        }

        public void Clear()
        {
            this.EnsureAllLoaded();
            IEnumerable<LoadedItem<T>> loadedItems = this.loadedArea.LoadedItems;
            foreach (var loadedItem in loadedItems)
            {
                this.stagingArea.Stage(loadedItem.Persistent, loadedItem.Loaded, OperationCodes.Remove);
            }
        }

        public bool Contains(T item)
        {
            return this.dirtyArea.DirtyItems.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.CopyTo((Array)array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            this.dirtyArea.DirtyItems.ToArray().CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.EnsureAllLoaded();
            return this.dirtyArea.DirtyItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Remove(T item)
        {
            T persistentObj = this.loadedArea.GetFromLoaded(item);

            if (persistentObj != null)
            {
                this.stagingArea.Stage(persistentObj, item, OperationCodes.Remove);
                return true;
            }

            return false;
        }

        public void Update(T item)
        {
            T persistentObj = this.loadedArea.GetFromLoaded(item);

            if (persistentObj != null)
            {
                this.stagingArea.Stage(persistentObj, item, OperationCodes.Update);
            }
        }

        private void EnsureAllLoaded()
        {
            foreach (T originalObj in this.persistentArea)
            {
                this.loadedArea.Load(originalObj);
            }
        }
    }
}