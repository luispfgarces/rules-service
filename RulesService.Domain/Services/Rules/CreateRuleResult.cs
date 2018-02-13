using System.Collections.Generic;
using System.Linq;
using RulesService.Domain.Models;

namespace RulesService.Domain.Services.Rules
{
    public class CreateRuleResult
    {
        private readonly List<CreateRuleResultMessage> errorMessages;

        public CreateRuleResult()
        {
            this.errorMessages = new List<CreateRuleResultMessage>(0);
        }

        public Rule CreatedRule { get; set; }

        public IEnumerable<CreateRuleResultMessage> ErrorMessages => this.errorMessages.AsReadOnly();

        internal bool HasErrors => this.errorMessages.Any();

        public void AddErrorMessage(string code, string message)
        {
            CreateRuleResultMessage createRuleResultMessage = new CreateRuleResultMessage
            {
                Code = code,
                Message = message
            };

            this.errorMessages.Add(createRuleResultMessage);
        }
    }
}