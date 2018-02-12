using System;
using System.Linq;
using System.Reflection;
using RulesService.Domain.Models.Operators;

namespace RulesService.Domain.Models.Factories
{
    internal class OperatorFactory : IOperatorFactory
    {
        private static readonly Type operatorCodesType = typeof(OperatorCodes);

        public Operator CreateOperator(OperatorCodes operatorCode)
        {
            if (!Enum.IsDefined(operatorCodesType, operatorCode))
            {
                throw new ArgumentException(
                    FormattableString.Invariant($"The value provided is not a valid operator code: {(int)operatorCode}."),
                    nameof(operatorCode));
            }

            int code = (int)operatorCode;
            string name = operatorCode.ToString();
            string description = string.Empty;
            string symbols = string.Empty;

            MemberInfo literalMemberInfo = operatorCodesType.GetMember(name).First();
            description = this.GetDescription(literalMemberInfo);
            symbols = this.GetSymbols(literalMemberInfo);

            return new Operator(code, name)
            {
                Description = description,
                Symbols = symbols
            };
        }

        private string GetDescription(MemberInfo literalMemberInfo)
        {
            OperatorDescriptionAttribute operatorDescriptionAttribute = literalMemberInfo.GetCustomAttribute<OperatorDescriptionAttribute>();
            return operatorDescriptionAttribute?.Description;
        }

        private string GetSymbols(MemberInfo literalMemberInfo)
        {
            OperatorSymbolsAttribute operatorSymbolsAttribute = literalMemberInfo.GetCustomAttribute<OperatorSymbolsAttribute>();
            return operatorSymbolsAttribute?.Symbol;
        }
    }
}