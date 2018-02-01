using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Domain.Core;

namespace RulesService.Data.InMemoryRepositories
{
    public class InMemoryRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TKey : new()
        where TEntity : EntityBase<TKey>
    {
        protected readonly List<TEntity> entities;

        public InMemoryRepositoryBase()
        {
            this.entities = new List<TEntity>(0);
        }

        public Task Add(TEntity entity)
        {
            if (this.entities.Any(t => object.Equals(t.Id, entity.Id)))
            {
                throw new InvalidOperationException("Given tenant already exists on repository.");
            }

            this.entities.Add(entity);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult<IEnumerable<TEntity>>(this.entities.AsReadOnly());
        }

        public Task<TEntity> GetById(TKey id)
        {
            return Task.FromResult(this.entities.SingleOrDefault(t => object.Equals(t.Id, id)));
        }

        public Task Remove(TEntity entity)
        {
            this.entities.Remove(entity);

            return Task.CompletedTask;
        }

        public Task Update(TEntity entity)
        {
            entity.AuditMetadata.StampAsModified(DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}