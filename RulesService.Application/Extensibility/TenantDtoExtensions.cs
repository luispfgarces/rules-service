using System;

namespace RulesService.Application.Dto.Tenants
{
    public static class TenantDtoExtensions
    {
        public static TenantDto ToTenantDto(this CreateTenantDto createTenantDto) => new TenantDto
        {
            Name = createTenantDto.Name
        };

        public static TenantDto ToTenantDto(this UpdateTenantDto updateTenantDto, Guid tenantId) => new TenantDto
        {
            Id = tenantId,
            Name = updateTenantDto.Name
        };
    }
}