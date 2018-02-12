using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesService.Application.Dto.Tenants;
using RulesService.Application.Exceptions;
using RulesService.Application.Services;

namespace RulesService.Presentation.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    public class TenantsController : Controller
    {
        private readonly ITenantService tenantService;

        public TenantsController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TenantDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromBody] CreateTenantDto createTenantDto)
        {
            TenantDto createdTenantDto = await this.tenantService.Add(createTenantDto.ToTenantDto());

            if (createdTenantDto.Id != Guid.Empty)
            {
                return this.CreatedAtRoute("get-tenant", new { id = createdTenantDto.Id }, createdTenantDto);
            }

            return this.BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TenantDto>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TenantDto> tenants = await this.tenantService.GetAll();

            return this.Ok(tenants);
        }

        [HttpGet, Route("{id}", Name = "get-tenant")]
        [ProducesResponseType(200, Type = typeof(TenantDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            TenantDto tenant = await this.tenantService.GetBy(id);

            if (tenant != null)
            {
                return this.Ok(tenant);
            }

            return this.NotFound("A tenant w/ the given id was not found");
        }

        [HttpDelete, Route("{id}", Name = "remove-tenant")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await this.tenantService.Remove(id);

                return this.Ok();
            }
            catch (NotFoundException)
            {
                return this.NotFound("A tenant w/ the given id was not found");
            }
        }

        [HttpPut, Route("{id}", Name = "update-tenant")]
        [ProducesResponseType(200, Type = typeof(TenantDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTenantDto updateTenantDto)
        {
            try
            {
                TenantDto updatedTenantDto = await this.tenantService.Update(updateTenantDto.ToTenantDto(id));

                return this.Ok(updatedTenantDto);
            }
            catch (NotFoundException)
            {
                return this.NotFound("A tenant w/ the given id was not found");
            }
        }
    }
}