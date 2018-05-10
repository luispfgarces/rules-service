using System;
using System.Collections.Generic;
using RulesService.Domain.Core;

namespace RulesService.Domain.Models
{
    public class Rule : EntityBase<Rule, RuleKey>
    {
        public Rule(Guid tenantId)
            : base()
        {
            this.Key = RuleKey.New(tenantId, Guid.NewGuid());
        }

        public string Content { get; set; }

        public int ContentTypeCode { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public RuleKey Key { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public IConditionNode RootCondition { get; set; }

        public override bool EqualsIdentity(Rule other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EqualsIdentity(other.Key);
        }

        public override bool EqualsIdentity(RuleKey key) => this.Key == key;

        public bool Matches(IDictionary<string, object> inputConditions)
        {
            throw new NotImplementedException();
        }
    }
}