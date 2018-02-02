using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Application.Dto.ConditionTypes;

namespace RulesService.Application.Services
{
    public interface IConditionTypeService
    {
        Task<ConditionTypeDto> Add(ConditionTypeDto conditionTypeDto);

        Task<IEnumerable<ConditionTypeDto>> GetAll(Guid tenantId);

        Task<ConditionTypeDto> GetBy(Guid tenantId, int code);

        Task Remove(Guid tenantId, int code);

        Task<ConditionTypeDto> Update(ConditionTypeDto conditionTypeDto);
    }
}