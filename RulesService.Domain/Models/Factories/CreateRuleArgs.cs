using System;

namespace RulesService.Domain.Models.Factories
{
    public struct CreateRuleArgs
    {
        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public IConditionNode RootCondition { get; set; }

        public Guid TenantId { get; set; }
    }
}