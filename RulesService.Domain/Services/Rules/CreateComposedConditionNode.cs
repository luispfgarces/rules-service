using System.Collections.Generic;

namespace RulesService.Domain.Services.Rules
{
    public class CreateComposedConditionNode : CreateConditionNodeBase
    {
        public CreateComposedConditionNode()
            : base()
        {
        }

        public IEnumerable<CreateConditionNodeBase> ChildNodes { get; set; }
    }
}