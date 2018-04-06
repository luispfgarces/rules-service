using System;
using System.Collections.Generic;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal interface IDirtyArea<T> : IObserver<LoadedItem<T>>, IObserver<StagedItem<T>>
        where T : class
    {
        IEnumerable<T> DirtyItems { get; }

        void RegisterLoadedArea(ILoadedArea<T> loadedArea);

        void RegisterStagingArea(IStagingArea<T> stagingArea);
    }
}