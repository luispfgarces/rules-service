using System;
using System.Collections.Generic;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal interface IStagingArea<T> : IObservable<StagedItem<T>>
        where T : class
    {
        IEnumerable<StagedItem<T>> StagedItems { get; }

        T GetFromOriginal(T obj);

        T GetFromStaged(T obj);

        void Reset();

        void Stage(T originalObj, T stagedObj, OperationCodes operationCode);
    }
}