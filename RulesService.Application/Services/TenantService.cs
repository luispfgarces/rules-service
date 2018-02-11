using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.Dto;
using RulesService.Application.Exceptions;
using RulesService.Domain.Models;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;

namespace RulesService.Application.Services
{
    internal class TenantService : ITenantService
    {
        private readonly ITenantFactory tenantFactory;

        private readonly ITenantRepository tenantRepository;

        public TenantService(
            ITenantRepository tenantRepository,
            ITenantFactory tenantFactory)
        {
            this.tenantRepository = tenantRepository;
            this.tenantFactory = tenantFactory;
        }

        public async Task<TenantDto> Add(TenantDto tenantDto)
        {
            if (tenantDto == null)
            {
                throw new ArgumentNullException(nameof(tenantDto));
            }

            Tenant tenant = this.tenantFactory.CreateTenant(tenantDto.Name);

            await this.tenantRepository.Add(tenant);

            tenantDto.Id = tenant.Id;

            return tenantDto;
        }

        public async Task<IEnumerable<TenantDto>> GetAll()
        {
            return await tenantRepository.GetAll()
                .ContinueWith(tenants =>
                {
                    return tenants.GetAwaiter().GetResult().Select(t => this.ConvertToDto(t));
                });
        }

        public async Task<TenantDto> GetBy(Guid id)
        {
            return await tenantRepository.GetById(id)
                .ContinueWith(tenantTask =>
                {
                    Tenant tenant = tenantTask.GetAwaiter().GetResult();

                    if (tenant != null)
                    {
                        return this.ConvertToDto(tenant);
                    }

                    return null;
                });
        }

        public async Task Remove(Guid id)
        {
            Tenant tenant = await this.tenantRepository.GetById(id);

            if (tenant == null)
            {
                throw new NotFoundException(FormattableString.Invariant($"{nameof(Tenant)} was not found. Id = {id}"));
            }

            await this.tenantRepository.Remove(tenant);
        }

        public async Task<TenantDto> Update(TenantDto tenantDto)
        {
            Tenant tenant = await this.tenantRepository.GetById(tenantDto.Id);

            if (tenant == null)
            {
                throw new NotFoundException(FormattableString.Invariant($"{nameof(Tenant)} was not found. Id = {tenantDto.Id}"));
            }

            tenant.Name = tenantDto.Name;

            return await this.tenantRepository.Update(tenant)
                .ContinueWith(tenantTask =>
                {
                    tenantTask.GetAwaiter().GetResult();
                    return this.ConvertToDto(tenant);
                });
        }

        private TenantDto ConvertToDto(Tenant tenant) => new TenantDto
        {
            Id = tenant.Id,
            Name = tenant.Name
        };
    }
}