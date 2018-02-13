using System;

namespace RulesService.Application.Dto.Rules
{
    public class CreateRuleDto : RuleBaseDto
    {
        public int ContentTypeCode { get; set; }

        public DateTime DateBegin { get; set; }

        public string Name { get; set; }

        public ConditionNodeBaseDto RootCondition { get; set; }
    }
}