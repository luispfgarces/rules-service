using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.Dto.ConditionTypes;
using RulesService.Application.Exceptions;
using RulesService.Domain.Models;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;

namespace RulesService.Application.Services
{
    internal class ConditionTypeService : IConditionTypeService
    {
        private readonly IConditionTypeFactory conditionTypeFactory;

        private readonly IConditionTypeRepository conditionTypeRepository;

        public ConditionTypeService(
            IConditionTypeRepository conditionTypeRepository,
            IConditionTypeFactory conditionTypeFactory)
        {
            this.conditionTypeRepository = conditionTypeRepository;
            this.conditionTypeFactory = conditionTypeFactory;
        }

        public async Task<ConditionTypeDto> Add(ConditionTypeDto conditionTypeDto)
        {
            if (conditionTypeDto == null)
            {
                throw new ArgumentNullException(nameof(conditionTypeDto));
            }

            ConditionType conditionType = this.conditionTypeFactory.CreateConditionType(
                conditionTypeDto.TenantId,
                conditionTypeDto.Code,
                conditionTypeDto.DataTypeCode.AsDataTypeCode(),
                conditionTypeDto.Name,
                conditionTypeDto.Description);

            await this.conditionTypeRepository.Add(conditionType);

            conditionTypeDto.Code = conditionType.Key.Code;
            conditionTypeDto.TenantId = conditionType.Key.TenantId;

            return conditionTypeDto;
        }

        public async Task<IEnumerable<ConditionTypeDto>> GetAll(Guid tenantId)
        {
            IEnumerable<ConditionType> conditionTypes = await this.conditionTypeRepository.GetAll();

            return conditionTypes.Where(ct => ct.Key.TenantId == tenantId).Select(ct => this.ConvertToDto(ct)).ToList();
        }

        public async Task<ConditionTypeDto> GetBy(Guid tenantId, int code)
        {
            if (tenantId == Guid.Empty)
            {
                throw new ArgumentException($"The provided tenant Id is invalid: { tenantId }", nameof(tenantId));
            }

            if (code <= 0)
            {
                throw new ArgumentException($"The provided condition type code is invalid: { code }", nameof(code));
            }

            ConditionTypeKey key = ConditionTypeKey.New(tenantId, code);
            return await this.conditionTypeRepository.GetById(key)
                .ContinueWith(conditionTypeTask =>
                {
                    ConditionType conditionType = conditionTypeTask.GetAwaiter().GetResult();

                    if (conditionType != null)
                    {
                        return this.ConvertToDto(conditionType);
                    }

                    return null;
                });
        }

        public async Task Remove(Guid tenantId, int code)
        {
            ConditionTypeKey key = ConditionTypeKey.New(tenantId, code);
            ConditionType conditionType = await this.conditionTypeRepository.GetById(key);

            if (conditionType == null)
            {
                throw new NotFoundException(FormattableString.Invariant($"{nameof(ConditionType)} was not found. Key = {key}"));
            }

            await this.conditionTypeRepository.Remove(conditionType);
        }

        public async Task<ConditionTypeDto> Update(ConditionTypeDto conditionTypeDto)
        {
            ConditionTypeKey key = ConditionTypeKey.New(conditionTypeDto.TenantId, conditionTypeDto.Code);
            ConditionType conditionType = await this.conditionTypeRepository.GetById(key);

            if (conditionType == null)
            {
                throw new NotFoundException(FormattableString.Invariant($"{nameof(ConditionType)} was not found. Key = {key}"));
            }

            conditionType.Name = conditionTypeDto.Name;
            conditionType.Description = conditionTypeDto.Description;

            return await this.conditionTypeRepository.Update(conditionType)
                .ContinueWith(tenantTask =>
                {
                    tenantTask.GetAwaiter().GetResult();
                    return this.ConvertToDto(conditionType);
                });
        }

        private ConditionTypeDto ConvertToDto(ConditionType ct) => new ConditionTypeDto
        {
            Code = ct.Key.Code,
            DataTypeCode = ct.DataTypeCode.AsInteger(),
            Description = ct.Description,
            Name = ct.Name,
            TenantId = ct.Key.TenantId
        };
    }
}