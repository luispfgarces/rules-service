using System;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Data.InMemoryRepositories
{
    internal class ContentTypeInMemoryRepository : InMemoryRepositoryBase<ContentType, ContentTypeKey>, IContentTypeRepository
    {
        public ContentTypeInMemoryRepository(ITenantRepository tenantRepository)
        {
            this.entities.AddRange(new[]
            {
                new ContentType(Guid.Parse("d29c8e8b-0e46-4993-9c77-25e48bdd6691"), 1, "Rate"),
                new ContentType(Guid.Parse("2999babb-16ea-446c-84d5-d08093377e48"), 1, "Delivery Date")
            });
        }
    }
}