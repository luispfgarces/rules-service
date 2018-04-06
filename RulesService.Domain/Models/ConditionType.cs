using System;
using RulesService.Domain.Core;

namespace RulesService.Domain.Models
{
    public class ConditionType : EntityBase<ConditionType, ConditionTypeKey>
    {
        public ConditionType(Guid tenantId, int code, DataTypeCodes dataTypeCode)
        {
            this.DataTypeCode = dataTypeCode;
            this.Key = new ConditionTypeKey
            {
                Code = code,
                TenantId = tenantId
            };
        }

        private ConditionType()
        {
        }

        public DataTypeCodes DataTypeCode { get; private set; }

        public string Description { get; set; }

        public ConditionTypeKey Key { get; private set; }

        public string Name { get; set; }

        public override bool EqualsIdentity(ConditionType other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsIdentity(other.Key);
        }

        public override bool EqualsIdentity(ConditionTypeKey key) => this.Key == key;
    }
}