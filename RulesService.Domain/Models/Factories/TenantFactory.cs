using System;

namespace RulesService.Domain.Models.Factories
{
    internal class TenantFactory : ITenantFactory
    {
        public Tenant CreateTenant(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A valid tenant name must be provided.", nameof(name));
            }

            return new Tenant
            {
                Name = name
            };
        }
    }
}