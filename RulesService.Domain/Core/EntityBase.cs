using System;

namespace RulesService.Domain.Core
{
    public abstract class EntityBase<TConcrete, TKey>
        where TKey : new()
        where TConcrete : EntityBase<TConcrete, TKey>
    {
        protected EntityBase()
        {
            DateTime now = DateTime.UtcNow;

            this.AuditMetadata = new AuditMetadata(now);
        }

        public AuditMetadata AuditMetadata { get; private set; }

        public abstract bool EqualsIdentity(TConcrete other);

        public abstract bool EqualsIdentity(TKey key);
    }
}