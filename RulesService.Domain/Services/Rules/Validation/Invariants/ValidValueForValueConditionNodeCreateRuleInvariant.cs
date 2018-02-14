using System;
using RulesService.Domain.Models;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class ValidValueForValueConditionNodeCreateRuleInvariant : ValueConditionNodeCreateRuleInvariantTemplate
    {
        private const string CodeConst = "R010";

        public override string Code => ValidValueForValueConditionNodeCreateRuleInvariant.CodeConst;

        protected override string ValidateCurrent(CreateValueConditionNode createValueConditionNode, Guid tenantId)
        {
            DataTypeCodes dataTypeCode = (DataTypeCodes)createValueConditionNode.DataTypeCode;
            if (Enum.IsDefined(typeof(DataTypeCodes), dataTypeCode) && !this.IsValidDataTypeValue(dataTypeCode, createValueConditionNode.RightHandOperand))
            {
                return string.Format(InvariantResources.R010, dataTypeCode.ToString(), createValueConditionNode.RightHandOperand?.ToString());
            }

            return null;
        }

        private bool IsValidDataTypeValue(DataTypeCodes dataTypeCode, object value)
        {
            string valueStringRepresentation = value?.ToString() ?? string.Empty;
            switch (dataTypeCode)
            {
                case DataTypeCodes.Integer:
                    return Int32.TryParse(valueStringRepresentation, out int integer);

                case DataTypeCodes.Decimal:
                    return Decimal.TryParse(valueStringRepresentation, out decimal @decimal);

                case DataTypeCodes.String:
                    return value != null;

                default:
                    throw new NotSupportedException("Unsupported data type.");
            }
        }
    }
}