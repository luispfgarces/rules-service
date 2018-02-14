using System;

namespace RulesService.Domain.Services.Rules
{
    public class CreateRule
    {
        public int ContentTypeCode { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public CreateConditionNodeBase RootCondition { get; set; }

        public Guid TenantId { get; set; }
    }
}