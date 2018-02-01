using System;
using System.Collections.Generic;
using System.Text;
using RulesService.Domain.Core;
using RulesService.Domain.Models;

namespace RulesService.Domain.Model
{
    public class Rule : EntityBase<Guid>
    {
        public Rule()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        public IEnumerable<RuleCondition> Conditions { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public Tenant Tenant { get; set; }

        public bool Matches(IDictionary<string, object> inputConditions)
        {
            throw new NotImplementedException();
        }
    }
}
