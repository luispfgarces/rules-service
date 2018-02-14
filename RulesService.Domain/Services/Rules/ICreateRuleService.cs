using System.Threading.Tasks;

namespace RulesService.Domain.Services.Rules
{
    public interface ICreateRuleService
    {
        Task<CreateRuleResult> CreateRule(CreateRule createRule);
    }
}