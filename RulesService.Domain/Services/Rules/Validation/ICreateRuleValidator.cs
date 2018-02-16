using System.Collections.Generic;

namespace RulesService.Domain.Services.Rules.Validation
{
    internal interface ICreateRuleValidator
    {
        IEnumerable<RuleValidationMessage> Validate(CreateRule createRule);
    }
}