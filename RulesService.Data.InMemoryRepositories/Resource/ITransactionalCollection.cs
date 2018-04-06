using System.Collections;
using System.Collections.Generic;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal interface ITransactionalCollection<T> : ICollection<T>, IReadOnlyCollection<T>, ICollection
    {
        void Update(T item);
    }
}