using System;
using System.Collections.Generic;
using System.Text;

namespace RulesService.Domain.Models
{
    public class RuleConditionValue
    {
        public OperatorCodes Operator { get; set; }

        public object RightOperand { get; set; }
    }
}
