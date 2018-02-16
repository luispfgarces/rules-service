using System.Collections.Generic;
using System.Linq;
using RulesService.Domain.Models;

namespace RulesService.Domain.Services.Rules
{
    public class RuleResult
    {
        private readonly List<RuleResultMessage> errorMessages;

        public RuleResult()
        {
            this.errorMessages = new List<RuleResultMessage>(0);
        }

        public Rule AffectedRule { get; set; }

        public IEnumerable<RuleResultMessage> ErrorMessages => this.errorMessages.AsReadOnly();

        internal bool HasErrors => this.errorMessages.Any();

        public void AddErrorMessage(string code, string message)
        {
            RuleResultMessage createRuleResultMessage = new RuleResultMessage
            {
                Code = code,
                Message = message
            };

            this.errorMessages.Add(createRuleResultMessage);
        }
    }
}