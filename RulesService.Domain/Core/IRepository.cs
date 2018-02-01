using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesService.Domain.Core
{
    public interface IRepository<TEntity, TKey>
        where TKey : new()
        where TEntity : EntityBase<TKey>
    {
        Task Add(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(TKey id);

        Task Remove(TEntity entity);

        Task Update(TEntity entity);
    }
}