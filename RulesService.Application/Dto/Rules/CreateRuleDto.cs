using System;

namespace RulesService.Application.Dto.Rules
{
    public class CreateRuleDto : RuleBaseDto
    {
        public Guid ContentTypeId { get; set; }

        public DateTime DateBegin { get; set; }

        public string Name { get; set; }
    }
}