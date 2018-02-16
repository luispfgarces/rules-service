using System.Collections.Generic;

namespace RulesService.Domain.Services.Rules.Validation
{
    internal interface IUpdateRuleValidator
    {
        IEnumerable<RuleValidationMessage> Validate(UpdateRule createRule);
    }
}