using System;
using RulesService.Domain.Models.ConditionNodes;

namespace RulesService.Domain.Models.Factories
{
    internal class ValueConditionNodeFactory : IValueConditionNodeFactory
    {
        public IValueConditionNode CreateValueConditionNode(ConditionType conditionType, DataTypeCodes dataTypeCode, OperatorCodes operatorCode, object rightHandOperand)
        {
            if (conditionType == null)
            {
                throw new ArgumentNullException(nameof(conditionType));
            }

            if (!Enum.IsDefined(typeof(DataTypeCodes), dataTypeCode))
            {
                throw new ArgumentException("Specified invalid value for data type code.", nameof(dataTypeCode));
            }

            if (!Enum.IsDefined(typeof(OperatorCodes), operatorCode))
            {
                throw new ArgumentException("Specified invalid value for operator code.", nameof(operatorCode));
            }

            if (rightHandOperand == null)
            {
                throw new ArgumentNullException(nameof(rightHandOperand));
            }

            switch (dataTypeCode)
            {
                case DataTypeCodes.Integer:
                    return new IntegerConditionNode(conditionType.Key.Code, operatorCode, Convert.ToInt32(rightHandOperand));

                case DataTypeCodes.Decimal:
                    return new DecimalConditionNode(conditionType.Key.Code, operatorCode, Convert.ToDecimal(rightHandOperand));

                case DataTypeCodes.String:
                    return new StringConditionNode(conditionType.Key.Code, operatorCode, rightHandOperand.ToString());

                default:
                    throw new NotSupportedException("Unsupported data type.");
            }
        }
    }
}