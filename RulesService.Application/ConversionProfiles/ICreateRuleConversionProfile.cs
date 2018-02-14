using System;
using RulesService.Application.Dto.Rules;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.ConversionProfiles
{
    internal interface ICreateRuleConversionProfile
    {
        CreateRule Convert(Guid tenantId, CreateRuleDto createRuleDto);
    }
}