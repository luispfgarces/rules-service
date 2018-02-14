using System;
using RulesService.Application.Dto.Rules;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.ConversionProfiles
{
    internal class CreateRuleConversionProfile : ICreateRuleConversionProfile
    {
        private readonly IConditionNodeConversionProfile conditionNodeConversionProfile;

        public CreateRuleConversionProfile(IConditionNodeConversionProfile conditionNodeConversionProfile)
        {
            this.conditionNodeConversionProfile = conditionNodeConversionProfile;
        }

        public CreateRule Convert(Guid tenantId, CreateRuleDto createRuleDto)
        {
            return new CreateRule
            {
                ContentTypeCode = createRuleDto.ContentTypeCode,
                DateBegin = createRuleDto.DateBegin,
                DateEnd = createRuleDto.DateEnd,
                Name = createRuleDto.Name,
                Priority = createRuleDto.Priority,
                RootCondition = createRuleDto.RootCondition != null ? this.conditionNodeConversionProfile.Convert(createRuleDto.RootCondition) : null,
                TenantId = tenantId
            };
        }
    }
}