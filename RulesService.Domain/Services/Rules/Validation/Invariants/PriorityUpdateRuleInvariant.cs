namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class PriorityUpdateRuleInvariant : IUpdateRuleInvariant
    {
        private const string CodeConst = "R003";

        public string Code => PriorityUpdateRuleInvariant.CodeConst;

        public InvariantResult IsValid(UpdateRule obj)
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