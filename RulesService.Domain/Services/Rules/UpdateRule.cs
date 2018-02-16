using System;

namespace RulesService.Domain.Services.Rules
{
    public class UpdateRule
    {
        public DateTime? DateEnd { get; set; }

        public Guid Id { get; set; }

        public int Priority { get; set; }

        public Guid TenantId { get; set; }
    }
}