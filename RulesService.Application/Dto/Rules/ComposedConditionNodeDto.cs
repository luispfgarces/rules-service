using System.Collections.Generic;

namespace RulesService.Application.Dto.Rules
{
    public class ComposedConditionNodeDto : ConditionNodeBaseDto
    {
        public IEnumerable<ConditionNodeBaseDto> ChildNodes { get; set; }
    }
}