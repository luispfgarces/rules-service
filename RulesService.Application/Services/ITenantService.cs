using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Application.Dto;

namespace RulesService.Application.Services
{
    public interface ITenantService
    {
        Task<TenantDto> Add(TenantDto tenantDto);

        Task<IEnumerable<TenantDto>> GetAll();

        Task<TenantDto> GetBy(Guid id);

        Task Remove(Guid id);

        Task<TenantDto> Update(TenantDto tenantDto);
    }
}