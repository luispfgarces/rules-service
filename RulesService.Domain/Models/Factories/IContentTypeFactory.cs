using System;

namespace RulesService.Domain.Models.Factories
{
    public interface IContentTypeFactory
    {
        ContentType CreateContentType(Guid tenantId, int code, string name);
    }
}