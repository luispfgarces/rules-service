namespace RulesService.Domain.Models.RuleConditions
{
    public class StringRuleCondition : RuleCondition
    {
        public override DataTypeCodes DataType => DataTypeCodes.String;
    }
}