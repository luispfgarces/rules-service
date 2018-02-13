using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Application.Dto.Common;
using RulesService.Application.Dto.Rules;

namespace RulesService.Application.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<RuleDto>> GetAll(Guid tenantId, RulesFilterDto rulesFilterDto, PaginationDto paginationDto);

        Task<RuleDto> GetBy(Guid tenantId, Guid id);
    }
}