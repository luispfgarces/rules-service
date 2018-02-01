using System;
using System.Collections.Generic;
using System.Text;

namespace RulesService.Domain.Models
{
    public abstract class RuleCondition
    {
        public IEnumerable<RuleConditionValue> ConditionValues { get; set; }

        public abstract DataTypeCodes DataType { get; }

        public Guid Id { get; set; }
    }
}
