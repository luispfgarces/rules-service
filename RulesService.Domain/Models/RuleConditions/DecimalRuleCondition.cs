using System;
using System.Collections.Generic;
using System.Text;

namespace RulesService.Domain.Models.RuleConditions
{
    public class DecimalRuleCondition : RuleCondition
    {
        public override DataTypeCodes DataType => DataTypeCodes.Decimal;
    }
}
