using System;

namespace RulesService.Domain.Models.Operators
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class OperatorSymbolsAttribute : Attribute
    {
        public OperatorSymbolsAttribute(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            this.Symbol = symbol;
        }

        public string Symbol { get; private set; }
    }
}