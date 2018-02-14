namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class PriorityCreateRuleInvariant : ICreateRuleInvariant
    {
        private const string CodeConst = "R003";

        public string Code => PriorityCreateRuleInvariant.CodeConst;

        public InvariantResult IsValid(CreateRule obj)
        {
            // Validate rule priority.
            if (obj.Priority <= 0)
            {
                return InvariantResult.ForInvalid(this.Code, string.Format(InvariantResources.R003, obj.Priority));
            }

            return InvariantResult.ForValid(this.Code);
        }
    }
}