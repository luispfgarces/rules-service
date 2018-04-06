using System;
using System.Collections.Generic;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal interface ILoadedArea<T> : IObservable<LoadedItem<T>>
        where T : class
    {
        IEnumerable<LoadedItem<T>> LoadedItems { get; }

        T GetFromLoaded(T obj);

        T GetFromOriginal(T obj);

        IEnumerator<T> GetLoadedEnumerator();

        void Load(T obj);

        void Reset();
    }
}