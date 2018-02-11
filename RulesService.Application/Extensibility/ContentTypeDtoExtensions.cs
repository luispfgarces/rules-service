using System;

namespace RulesService.Application.Dto.ContentTypes
{
    public static class ContentTypeDtoExtensions
    {
        public static ContentTypeDto ToConditionTypeDto(this CreateContentTypeDto createContentTypeDto, Guid tenantId) => new ContentTypeDto
        {
            Code = createContentTypeDto.Code,
            Name = createContentTypeDto.Name,
            TenantId = tenantId
        };

        public static ContentTypeDto ToConditionTypeDto(this UpdateContentTypeDto updateContentTypeDto, Guid tenantId, int code) => new ContentTypeDto
        {
            Code = code,
            Name = updateContentTypeDto.Name,
            TenantId = tenantId
        };
    }
}