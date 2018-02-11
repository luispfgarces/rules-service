using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesService.Application.Dto.ContentTypes;
using RulesService.Application.Exceptions;
using RulesService.Application.Services;

namespace RulesService.Presentation.Api.Controllers.V1
{
    [Route("api/v1/tenants/{tenantId}/[controller]")]
    public class ContentTypesController : Controller
    {
        private readonly IContentTypeService contentTypeService;

        public ContentTypesController(IContentTypeService contentTypeService)
        {
            this.contentTypeService = contentTypeService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ContentTypeDto))]
        public async Task<IActionResult> Add([FromRoute] Guid tenantId, [FromBody] CreateContentTypeDto createContentTypeDto)
        {
            ContentTypeDto contentTypeDto = await this.contentTypeService.Add(createContentTypeDto.ToConditionTypeDto(tenantId));

            return this.CreatedAtRoute("get-condition-type", new { tenantId = contentTypeDto.TenantId, code = contentTypeDto.Code }, contentTypeDto);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromRoute] Guid tenantId)
        {
            IEnumerable<ContentTypeDto> contentTypeDtos = await this.contentTypeService.GetAll(tenantId);

            return this.Ok(contentTypeDtos);
        }

        [HttpGet, Route("{code}", Name = "get-content-type")]
        [ProducesResponseType(200, Type = typeof(ContentTypeDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBy([FromRoute] Guid tenantId, [FromRoute] int code)
        {
            ContentTypeDto contentTypeDto = await this.contentTypeService.GetBy(tenantId, code);

            if (contentTypeDto != null)
            {
                return this.Ok(contentTypeDto);
            }

            return this.NotFound("A content type w/ the given tenant id and code was not found");
        }

        [HttpPut, Route("{code}", Name = "update-content-type")]
        [ProducesResponseType(200, Type = typeof(ContentTypeDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] Guid tenantId, [FromRoute] int code, [FromBody] UpdateContentTypeDto updateContentTypeDto)
        {
            try
            {
                ContentTypeDto conditionTypeDto = await this.contentTypeService.Update(updateContentTypeDto.ToConditionTypeDto(tenantId, code));

                return this.Ok(conditionTypeDto);
            }
            catch (NotFoundException)
            {
                return this.NotFound("A content type w/ the given id was not found");
            }
        }
    }
}