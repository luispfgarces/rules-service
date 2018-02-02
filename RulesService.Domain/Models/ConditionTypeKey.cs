using System;
using System.Collections.Generic;

namespace RulesService.Domain.Models
{
    public struct ConditionTypeKey
    {
        public int Code { get; set; }

        public Guid TenantId { get; set; }

        public static ConditionTypeKey New(Guid tenantId, int code) => new ConditionTypeKey
        {
            Code = code,
            TenantId = tenantId
        };

        public static bool operator !=(ConditionTypeKey leftHand, ConditionTypeKey rightHand) => !(leftHand == rightHand);

        public static bool operator ==(ConditionTypeKey leftHand, ConditionTypeKey rightHand) => leftHand.Code == rightHand.Code && leftHand.TenantId == rightHand.TenantId;

        public override bool Equals(object obj)
        {
            if (!(obj is ConditionTypeKey))
            {
                return false;
            }

            var key = (ConditionTypeKey)obj;
            return Code == key.Code &&
                   TenantId.Equals(key.TenantId);
        }

        public override int GetHashCode()
        {
            var hashCode = 1924547636;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Code.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(TenantId);
            return hashCode;
        }

        public override string ToString()
        {
            return FormattableString.Invariant($"{{{nameof(this.TenantId)}: {this.TenantId} , {nameof(this.Code)}: {this.Code}}}");
        }
    }
}