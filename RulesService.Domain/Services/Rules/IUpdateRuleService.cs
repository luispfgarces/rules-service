using System.Threading.Tasks;

namespace RulesService.Domain.Services.Rules
{
    public interface IUpdateRuleService
    {
        Task<RuleResult> UpdateRule(UpdateRule updateRule);
    }
}