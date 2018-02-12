using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesService.Application.Dto.Operators;
using RulesService.Application.Services;

namespace RulesService.Presentation.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    public class OperatorsController : Controller
    {
        private readonly IOperatorService operatorService;

        public OperatorsController(IOperatorService operatorService)
        {
            this.operatorService = operatorService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OperatorDto>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<OperatorDto> operatorDtos = await this.operatorService.GetAll();
            return this.Ok(operatorDtos);
        }

        [HttpGet, Route("{code}", Name = "get-operator")]
        [ProducesResponseType(200, Type = typeof(OperatorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBy([FromRoute] int code)
        {
            OperatorDto operatorDto = await this.operatorService.GetByCode(code);

            if (operatorDto != null)
            {
                return this.Ok(operatorDto);
            }

            return this.NotFound("A operator w/ the given code was not found");
        }
    }
}