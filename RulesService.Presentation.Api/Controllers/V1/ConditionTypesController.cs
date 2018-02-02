using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesService.Application.Dto.ConditionTypes;
using RulesService.Application.Exceptions;
using RulesService.Application.Services;

namespace RulesService.Presentation.Api.Controllers.V1
{
    [Route("api/v1/tenants/{tenantId}/[controller]")]
    public class ConditionTypesController : Controller
    {
        private readonly IConditionTypeService conditionTypeService;

        public ConditionTypesController(IConditionTypeService conditionTypeService)
        {
            this.conditionTypeService = conditionTypeService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateConditionTypeDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromRoute] Guid tenantId, [FromBody] CreateConditionTypeDto conditionTypeDto)
        {
            ConditionTypeDto updatedConditionTypeDto = await this.conditionTypeService.Add(conditionTypeDto.ToConditionTypeDto(tenantId));

            if (updatedConditionTypeDto.Code != default(int))
            {
                return this.CreatedAtRoute("get-condition-type", new { code = updatedConditionTypeDto.Code }, updatedConditionTypeDto);
            }

            return this.BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ConditionTypeDto>))]
        public async Task<IActionResult> GetAll([FromRoute] Guid tenantId)
        {
            IEnumerable<ConditionTypeDto> conditionTypeDtos = await this.conditionTypeService.GetAll(tenantId);

            return this.Ok(conditionTypeDtos);
        }

        [HttpGet, Route("{code}", Name = "get-condition-type")]
        [ProducesResponseType(200, Type = typeof(ConditionTypeDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByKey([FromRoute] Guid tenantId, [FromRoute] int code)
        {
            ConditionTypeDto conditionTypeDto = await this.conditionTypeService.GetBy(tenantId, code);

            if (conditionTypeDto != null)
            {
                return this.Ok(conditionTypeDto);
            }

            return this.NotFound("A condition type w/ the given tenant id and code was not found");
        }

        [HttpPut, Route("{code}", Name = "update-condition-type")]
        [ProducesResponseType(200, Type = typeof(ConditionTypeDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] Guid tenantId, [FromRoute] int code, [FromBody] UpdateConditionTypeDto updateConditionTypeDto)
        {
            try
            {
                ConditionTypeDto conditionTypeDto = await this.conditionTypeService.Update(updateConditionTypeDto.ToConditionTypeDto(tenantId, code));

                return this.Ok(conditionTypeDto);
            }
            catch (NotFoundException)
            {
                return this.NotFound("A condition type w/ the given id was not found");
            }
        }
    }
}