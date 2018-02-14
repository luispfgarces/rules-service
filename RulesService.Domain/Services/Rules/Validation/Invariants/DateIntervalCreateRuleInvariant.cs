using System;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class DateIntervalCreateRuleInvariant : ICreateRuleInvariant
    {
        private const string CodeConst = "R002";

        public string Code => DateIntervalCreateRuleInvariant.CodeConst;

        public InvariantResult IsValid(CreateRule obj)
        {
            // Validate rule interval.
            if (obj.DateEnd.HasValue)
            {
                DateTime dateBegin = obj.DateBegin;
                DateTime dateEnd = obj.DateEnd.GetValueOrDefault();
                if (dateEnd <= dateBegin)
                {
                    return InvariantResult.ForInvalid(this.Code, string.Format(InvariantResources.R002, dateBegin, dateEnd));
                }
            }

            return InvariantResult.ForValid(this.Code);
        }
    }
}