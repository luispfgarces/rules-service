using System;
using RulesService.Application.Dto.Rules;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.ConversionProfiles
{
    internal interface IUpdateRuleConversionProfile
    {
        UpdateRule Convert(Guid tenantId, Guid id, UpdateRuleDto updateRuleDto);
    }
}