using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesService.Application.Dto.Common;
using RulesService.Application.Dto.Rules;
using RulesService.Application.Services;

namespace RulesService.Presentation.Api.Controllers.V1
{
    [Route("api/tenants/{tenantId}/[controller]")]
    public class RulesController : Controller
    {
        private readonly IRuleService ruleService;

        public RulesController(IRuleService ruleService)
        {
            this.ruleService = ruleService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RuleDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromRoute] Guid tenantId, [FromBody] CreateRuleDto createRuleDto)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RuleDto>))]
        public async Task<IActionResult> GetAll(
            [FromRoute] Guid tenantId,
            [FromQuery] int? contentTypeCode = null,
            [FromQuery] DateTime? filterDateBegin = null,
            [FromQuery] DateTime? filterDateEnd = null,
            [FromQuery] int? pageIndex = null,
            [FromQuery] int? pageSize = null)
        {
            RulesFilterDto rulesFilterDto = null;
            PaginationDto paginationDto = null;

            if (contentTypeCode != null || filterDateBegin != null || filterDateEnd != null)
            {
                rulesFilterDto = new RulesFilterDto
                {
                    ContentTypeCode = contentTypeCode,
                    FilterDateBegin = filterDateBegin,
                    FilterDateEnd = filterDateEnd
                };
            }

            if (pageIndex != null && pageSize != null)
            {
                paginationDto = new PaginationDto
                {
                    PageIndex = pageIndex.GetValueOrDefault(),
                    PageSize = pageSize.GetValueOrDefault()
                };
            }

            IEnumerable<RuleDto> ruleDtos = await this.ruleService.GetAll(tenantId, rulesFilterDto, paginationDto);

            return this.Ok(ruleDtos);
        }

        [HttpGet, Route("{id}", Name = "get-rule")]
        [ProducesResponseType(200, Type = typeof(RuleDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBy([FromRoute] Guid tenantId, [FromRoute] Guid id)
        {
            RuleDto ruleDto = await this.ruleService.GetBy(tenantId, id);

            if (ruleDto != null)
            {
                return this.Ok(ruleDto);
            }

            return this.NotFound("A rule with the given tenant id and id was not found.");
        }

        [HttpPut, Route("{id}", Name = "update-rule")]
        [ProducesResponseType(200, Type = typeof(RuleDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update([FromRoute] Guid tenantId, [FromRoute] Guid id, [FromBody] UpdateRuleDto updateRuleDto)
        {
            throw new NotImplementedException();
        }
    }
}