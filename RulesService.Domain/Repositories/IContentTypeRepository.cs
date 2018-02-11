using RulesService.Domain.Core;
using RulesService.Domain.Models;

namespace RulesService.Domain.Repositories
{
    public interface IContentTypeRepository : IRepository<ContentType, ContentTypeKey>
    {
    }
}