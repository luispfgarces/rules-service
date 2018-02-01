using System;

namespace RulesService.Domain.Core
{
    public class EntityBase<TKey>
        where TKey : new()
    {
        protected EntityBase()
        {
            DateTime now = DateTime.UtcNow;

            this.AuditMetadata = new AuditMetadata(now);
        }

        public AuditMetadata AuditMetadata { get; private set; }

        public TKey Id { get; set; }
    }
}