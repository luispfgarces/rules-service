using System;

namespace RulesService.Application.Dto.ConditionTypes
{
    public static class ConditionTypeDtoExtensions
    {
        public static ConditionTypeDto ToConditionTypeDto(this CreateConditionTypeDto createConditionTypeDto, Guid tenantId) => new ConditionTypeDto
        {
            Code = createConditionTypeDto.Code,
            DataTypeCode = createConditionTypeDto.DataTypeCode,
            Description = createConditionTypeDto.Description,
            Name = createConditionTypeDto.Name,
            TenantId = tenantId
        };

        public static ConditionTypeDto ToConditionTypeDto(this UpdateConditionTypeDto updateConditionTypeDto, Guid tenantId, int code) => new ConditionTypeDto
        {
            Code = code,
            DataTypeCode = updateConditionTypeDto.DataTypeCode,
            Description = updateConditionTypeDto.Description,
            Name = updateConditionTypeDto.Name,
            TenantId = tenantId
        };
    }
}