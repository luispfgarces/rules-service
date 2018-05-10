namespace RulesService.Domain.Models.Factories
{
    public interface IRuleFactory
    {
        Rule CreateRule(CreateRuleArgs createRuleArgs);
    }
}