using System.Collections.Generic;

namespace RulesService.Application.Dto.Rules
{
    public class RuleResultDto
    {
        public RuleDto AffectedRule { get; set; }

        public IEnumerable<object> ErrorMessages { get; set; }
    }
}