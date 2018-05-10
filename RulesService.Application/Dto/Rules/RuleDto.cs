using System;

namespace RulesService.Application.Dto.Rules
{
    public class RuleDto : RuleBaseDto
    {
        public string Content { get; set; }

        public int ContentTypeCode { get; set; }

        public DateTime DateBegin { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ConditionNodeBaseDto RootCondition { get; set; }

        public Guid TenantId { get; set; }
    }
}