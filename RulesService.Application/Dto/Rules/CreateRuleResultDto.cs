using System.Collections.Generic;

namespace RulesService.Application.Dto.Rules
{
    public class CreateRuleResultDto
    {
        public RuleDto CreatedRule { get; set; }

        public IEnumerable<object> ErrorMessages { get; set; }
    }
}