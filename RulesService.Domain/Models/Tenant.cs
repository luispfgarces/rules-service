using System;
using RulesService.Domain.Core;

namespace RulesService.Domain.Models
{
    public class Tenant : EntityBase<Tenant, Guid>
    {
        public Tenant()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public override bool EqualsIdentity(Tenant other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsIdentity(other.Id);
        }

        public override bool EqualsIdentity(Guid key) => this.Id == key;
    }
}