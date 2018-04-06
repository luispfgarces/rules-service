using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal class LoadedArea<T> : ILoadedArea<T>
        where T : class
    {
        private readonly IDeepCopyService deepCopyService;

        private readonly List<LoadedItem<T>> loadedItems;

        public LoadedArea(IDeepCopyService deepCopyService)
        {
            this.loadedItems = new List<LoadedItem<T>>(0);
            this.deepCopyService = deepCopyService;
        }

        public IEnumerable<LoadedItem<T>> LoadedItems => this.loadedItems.AsReadOnly();

        public T GetFromLoaded(T obj)
        {
            LoadedItem<T> loadedItem = this.loadedItems.SingleOrDefault(i => object.Equals(i.Loaded, obj));
            if (loadedItem != default(LoadedItem<T>))
            {
                return loadedItem.Persistent;
            }

            return null;
        }

        public T GetFromOriginal(T obj)
        {
            LoadedItem<T> loadedItem = this.loadedItems.SingleOrDefault(i => object.Equals(i.Persistent, obj));
            if (loadedItem != default(LoadedItem<T>))
            {
                return loadedItem.Loaded;
            }

            return null;
        }

        public IEnumerator<T> GetLoadedEnumerator()
        {
            return this.loadedItems.Select(i => i.Loaded).ToList().GetEnumerator();
        }

        public void Load(T obj)
        {
            if (!this.loadedItems.Any(i => object.Equals(i.Persistent, obj)))
            {
                T copy = (T)this.deepCopyService.Copy(obj);

                LoadedItem<T> loadedItem = new LoadedItem<T>
                {
                    Persistent = obj,
                    Loaded = copy
                };

                this.loadedItems.Add(loadedItem);
            }
        }

        public void Reset()
        {
            this.loadedItems.Clear();
        }

        public IDisposable Subscribe(IObserver<LoadedItem<T>> observer)
        {
            throw new NotImplementedException();
        }
    }
}