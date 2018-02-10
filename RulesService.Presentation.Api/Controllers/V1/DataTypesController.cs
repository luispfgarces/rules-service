using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesService.Application.Dto.DataTypes;
using RulesService.Application.Services;

namespace RulesService.Presentation.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    public class DataTypesController : Controller
    {
        private readonly IDataTypeService dataTypeService;

        public DataTypesController(IDataTypeService dataTypeService)
        {
            this.dataTypeService = dataTypeService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DataTypeDto>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<DataTypeDto> dataTypeDtos = await this.dataTypeService.GetAll();

            return this.Ok(dataTypeDtos);
        }

        [HttpGet, Route("{code}", Name = "get-data-type")]
        [ProducesResponseType(200, Type = typeof(DataTypeDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBy([FromRoute] int code)
        {
            DataTypeDto dataTypeDto = await this.dataTypeService.GetBy(code);

            if (dataTypeDto != null)
            {
                return this.Ok(dataTypeDto);
            }

            return this.NotFound("A data type w/ the given code was not found");
        }
    }
}