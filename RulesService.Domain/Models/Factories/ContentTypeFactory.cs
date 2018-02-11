using System;

namespace RulesService.Domain.Models.Factories
{
    public class ContentTypeFactory : IContentTypeFactory
    {
        public ContentType CreateContentType(Guid tenantId, int code, string name)
        {
            if (tenantId == default(Guid))
            {
                throw new ArgumentException("A valid tenant id must not be empty.", nameof(tenantId));
            }

            if (code <= 0)
            {
                throw new ArgumentException("A valid condition type id must be greater than 0.", nameof(code));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A valid name for the condition type was not provided.", nameof(name));
            }

            return new ContentType(tenantId, code, name);
        }
    }
}