using System.Threading.Tasks;

namespace RulesService.Domain.Services.Rules
{
    public interface ICreateRuleService
    {
        Task<RuleResult> CreateRule(CreateRule createRule);
    }
}