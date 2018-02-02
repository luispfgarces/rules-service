using System;

namespace RulesService.Application.Dto.ConditionTypes
{
    public class ConditionTypeDto : ConditionTypeBaseDto
    {
        public int Code { get; set; }

        public Guid TenantId { get; set; }
    }
}