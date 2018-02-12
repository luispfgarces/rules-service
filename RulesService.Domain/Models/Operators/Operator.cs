using System;

namespace RulesService.Domain.Models.Operators
{
    public class Operator
    {
        public Operator(int code, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Code = code;
            this.Name = name;
        }

        public int Code { get; private set; }

        public string Description { get; set; }

        public string Name { get; private set; }

        public OperatorCodes OperatorCode => (OperatorCodes)this.Code;

        public string Symbols { get; set; }
    }
}