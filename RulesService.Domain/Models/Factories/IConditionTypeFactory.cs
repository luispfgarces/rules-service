using System;

namespace RulesService.Domain.Models.Factories
{
    public interface IConditionTypeFactory
    {
        ConditionType CreateConditionType(Guid tenantId, int code, DataTypeCodes dataTypeCode, string name, string description);
    }
}