namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class NameCreateRuleInvariant : ICreateRuleInvariant
    {
        private const string CodeConst = "R001";

        public string Code => NameCreateRuleInvariant.CodeConst;

        public InvariantResult IsValid(CreateRule obj)
        {
            // Validate rule name.
            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                return InvariantResult.ForInvalid(this.Code, InvariantResources.R001);
            }

            return InvariantResult.ForValid(this.Code);
        }
    }
}