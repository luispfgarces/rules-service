using System;
using System.Linq;
using RulesService.Application.Dto.Rules;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.ConversionProfiles
{
    internal class ConditionNodeConversionProfile : IConditionNodeConversionProfile
    {
        public CreateConditionNodeBase Convert(ConditionNodeBaseDto conditionNodeBaseDto)
        {
            switch (conditionNodeBaseDto)
            {
                case ComposedConditionNodeDto ccn:
                    return new CreateComposedConditionNode
                    {
                        ChildNodes = ccn.ChildNodes.Select(n => this.Convert(n)),
                        LogicalOperatorCode = ccn.LogicalOperatorCode
                    };

                case ValueConditionNodeDto vcn:
                    return new CreateValueConditionNode
                    {
                        ConditionTypeCode = vcn.ConditionTypeCode,
                        DataTypeCode = vcn.DataTypeCode,
                        LogicalOperatorCode = vcn.LogicalOperatorCode,
                        OperatorCode = vcn.OperatorCode,
                        RightHandOperand = vcn.RightHandOperand
                    };

                default:
                    throw new NotSupportedException("Not supported condition node");
            }
        }
    }
}