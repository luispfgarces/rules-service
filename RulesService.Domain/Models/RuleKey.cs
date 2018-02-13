using System;
using System.Collections.Generic;

namespace RulesService.Domain.Models
{
    public struct RuleKey
    {
        public RuleKey(Guid tenantId, Guid id)
        {
            this.Id = id;
            this.TenantId = tenantId;
        }

        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public static RuleKey New(Guid tenantId, Guid id) => new RuleKey
        {
            Id = id,
            TenantId = tenantId
        };

        public static bool operator !=(RuleKey leftHand, RuleKey rightHand) => !(leftHand == rightHand);

        public static bool operator ==(RuleKey leftHand, RuleKey rightHand) => leftHand.Id == rightHand.Id && leftHand.TenantId == rightHand.TenantId;

        public override bool Equals(object obj)
        {
            if (!(obj is RuleKey))
            {
                return false;
            }

            var key = (RuleKey)obj;
            return Id == key.Id &&
                   TenantId.Equals(key.TenantId);
        }

        public override int GetHashCode()
        {
            var hashCode = 1924547636;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.TenantId);
            return hashCode;
        }

        public override string ToString()
        {
            return FormattableString.Invariant($"{{{nameof(this.TenantId)}: {this.TenantId} , {nameof(this.Id)}: {this.Id}}}");
        }
    }
}