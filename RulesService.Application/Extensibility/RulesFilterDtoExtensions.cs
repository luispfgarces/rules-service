using RulesService.Domain.Repositories;

namespace RulesService.Application.Dto.Rules
{
    internal static class RulesFilterDtoExtensions
    {
        public static RulesFilter ToRulesFilter(this RulesFilterDto rulesFilterDto)
        {
            return new RulesFilter
            {
                ContentTypeCode = rulesFilterDto.ContentTypeCode,
                FilterDateBegin = rulesFilterDto.FilterDateBegin,
                FilterDateEnd = rulesFilterDto.FilterDateEnd
            };
        }
    }
}