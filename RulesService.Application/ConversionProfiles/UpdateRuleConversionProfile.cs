using System;
using RulesService.Application.Dto.Rules;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.ConversionProfiles
{
    internal class UpdateRuleConversionProfile : IUpdateRuleConversionProfile
    {
        public UpdateRule Convert(Guid tenantId, Guid id, UpdateRuleDto updateRuleDto)
        {
            return new UpdateRule
            {
                DateEnd = updateRuleDto.DateEnd,
                Id = id,
                Priority = updateRuleDto.Priority,
                TenantId = tenantId
            };
        }
    }
}