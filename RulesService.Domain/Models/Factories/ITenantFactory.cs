using RulesService.Domain.Model;

namespace RulesService.Domain.Models.Factories
{
    public interface ITenantFactory
    {
        Tenant CreateTenant(string name);
    }
}