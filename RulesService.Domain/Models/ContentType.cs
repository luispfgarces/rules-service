using System;
using RulesService.Domain.Core;

namespace RulesService.Domain.Models
{
    public class ContentType : EntityBase<ContentType, ContentTypeKey>
    {
        public ContentType(Guid tenantId, int code, string name)
        {
            this.Key = new ContentTypeKey
            {
                Code = code,
                TenantId = tenantId
            };
            this.Name = name;
        }

        public ContentTypeKey Key { get; private set; }

        public string Name { get; set; }

        public override bool EqualsIdentity(ContentType other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsIdentity(other.Key);
        }

        public override bool EqualsIdentity(ContentTypeKey key) => this.Key == key;
    }
}