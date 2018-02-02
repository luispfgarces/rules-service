using System;
using System.Collections.Generic;
using RulesService.Domain.Core;
using RulesService.Domain.Models;

namespace RulesService.Domain.Model
{
    public class Rule : EntityBase<Rule, Guid>
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

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public Tenant Tenant { get; set; }

        public override bool EqualsIdentity(Rule other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsIdentity(other.Id);
        }

        public override bool EqualsIdentity(Guid key) => this.Id == key;

        public bool Matches(IDictionary<string, object> inputConditions)
        {
            throw new NotImplementedException();
        }
    }
}