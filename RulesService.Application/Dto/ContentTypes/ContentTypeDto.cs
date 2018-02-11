using System;

namespace RulesService.Application.Dto.ContentTypes
{
    public class ContentTypeDto : ContentTypeBaseDto
    {
        public int Code { get; set; }

        public Guid TenantId { get; set; }
    }
}