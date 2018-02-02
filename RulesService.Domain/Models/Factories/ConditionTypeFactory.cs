using System;

namespace RulesService.Domain.Models.Factories
{
    internal class ConditionTypeFactory : IConditionTypeFactory
    {
        private static Type DataTypeCodesType => typeof(DataTypeCodes);

        public ConditionType CreateConditionType(Guid tenantId, int code, DataTypeCodes dataTypeCode, string name, string description)
        {
            if (tenantId == default(Guid))
            {
                throw new ArgumentException("A valid tenant id must not be empty.", nameof(tenantId));
            }

            if (code <= 0)
            {
                throw new ArgumentException("A valid condition type id must be greater than 0.", nameof(code));
            }

            if (!Enum.IsDefined(ConditionTypeFactory.DataTypeCodesType, dataTypeCode))
            {
                throw new ArgumentException($"A unknown data type code was specified: {dataTypeCode.ToString()}", nameof(dataTypeCode));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A valid name for the condition type was not provided.", nameof(name));
            }

            return new ConditionType(tenantId, code, dataTypeCode)
            {
                Description = description,
                Name = name
            };
        }
    }
}