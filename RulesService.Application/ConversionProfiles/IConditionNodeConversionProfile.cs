using RulesService.Application.Dto.Rules;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.ConversionProfiles
{
    internal interface IConditionNodeConversionProfile
    {
        CreateConditionNodeBase Convert(ConditionNodeBaseDto conditionNodeBaseDto);
    }
}