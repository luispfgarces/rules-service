using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Application.Dto.ContentTypes;

namespace RulesService.Application.Services
{
    public interface IContentTypeService
    {
        Task<ContentTypeDto> Add(ContentTypeDto contentTypeDto);

        Task<IEnumerable<ContentTypeDto>> GetAll(Guid tenantId);

        Task<ContentTypeDto> GetBy(Guid tenantId, int code);

        Task<ContentTypeDto> Update(ContentTypeDto contentTypeDto);
    }
}