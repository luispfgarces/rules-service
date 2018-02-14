using System.Collections.Generic;
using System.Linq;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class InvariantResult
    {
        private InvariantResult()
        {
        }

        public string Code { get; set; }

        public IEnumerable<string> Messages { get; private set; }

        public bool Valid { get; private set; }

        public static InvariantResult ForInvalid(string code, params string[] messages) => new InvariantResult
        {
            Code = code,
            Messages = messages,
            Valid = false
        };

        public static InvariantResult ForValid(string code) => new InvariantResult
        {
            Code = code,
            Messages = Enumerable.Empty<string>(),
            Valid = true
        };
    }
}