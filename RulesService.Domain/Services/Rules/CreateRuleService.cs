using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Domain.Models;
using RulesService.Domain.Models.ConditionNodes;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;

namespace RulesService.Domain.Services.Rules
{
    internal class CreateRuleService : ICreateRuleService
    {
        private readonly IConditionNodeAbstractFactory conditionNodeAbstractFactory;
        private readonly IConditionTypeRepository conditionTypeRepository;
        private readonly IContentTypeRepository contentTypeRepository;
        private readonly IRuleFactory ruleFactory;
        private readonly IRuleRepository ruleRepository;
        private readonly ITenantRepository tenantRepository;

        public CreateRuleService(
            IConditionNodeAbstractFactory conditionNodeAbstractFactory,
            IConditionTypeRepository conditionTypeRepository,
            IContentTypeRepository contentTypeRepository,
            IRuleFactory ruleFactory,
            IRuleRepository ruleRepository,
            ITenantRepository tenantRepository)
        {
            this.conditionNodeAbstractFactory = conditionNodeAbstractFactory;
            this.conditionTypeRepository = conditionTypeRepository;
            this.contentTypeRepository = contentTypeRepository;
            this.ruleFactory = ruleFactory;
            this.ruleRepository = ruleRepository;
            this.tenantRepository = tenantRepository;
        }

        public async Task<CreateRuleResult> CreateRule(CreateRuleArgs createRuleArgs)
        {
            CreateRuleResult createRuleResult = new CreateRuleResult();

            // Validate rule name.
            if (string.IsNullOrWhiteSpace(createRuleArgs.Name))
            {
                createRuleResult.AddErrorMessage("RXXX", "Specified rule name is null or empty. Must input a valid rule name.");
            }

            // Validate rule interval.
            if (createRuleArgs.DateEnd.HasValue && createRuleArgs.DateEnd.GetValueOrDefault() <= createRuleArgs.DateBegin)
            {
                createRuleResult.AddErrorMessage("RXXX", $"Specified rule dates interval is invalid. (DateBegin = {createRuleArgs.DateBegin} | DateEnd = {createRuleArgs.DateEnd.GetValueOrDefault()})");
            }

            // Validate rule priority.
            if (createRuleArgs.Priority <= 0)
            {
                createRuleResult.AddErrorMessage("RXXX", $"Specified rule priority is invalid. (Priority = {createRuleArgs.Priority})");
            }

            // Fetch tenant. No need to validate, tenant should be validated at this point.
            Tenant tenant = await this.tenantRepository.GetById(createRuleArgs.TenantId);

            // Fetch content type and validate.
            ContentTypeKey contentTypeKey = ContentTypeKey.New(createRuleArgs.TenantId, createRuleArgs.ContentTypeCode);
            ContentType contentType = await this.contentTypeRepository.GetById(contentTypeKey);

            if (contentType == null)
            {
                createRuleResult.AddErrorMessage("RXXX", $"Specified content type does not exist. (TenantId = {createRuleArgs.TenantId} | ContentTypeCode = {createRuleArgs.ContentTypeCode})");
            }

            IConditionNode rootCondition = null;
            if (createRuleArgs.RootCondition != null)
            {
                rootCondition = await this.CreateConditionNodeRecursive(createRuleArgs.TenantId, createRuleArgs.RootCondition, createRuleResult);
            }

            if (!createRuleResult.HasErrors)
            {
                Rule rule = this.ruleFactory.CreateRule(
                        createRuleArgs.TenantId,
                        createRuleArgs.Name,
                        contentType,
                        createRuleArgs.DateBegin,
                        createRuleArgs.DateEnd,
                        createRuleArgs.Priority,
                        rootCondition);

                IEnumerable<Rule> existentRules = await this.ruleRepository.GetAll(
                    createRuleArgs.TenantId,
                    new RulesFilter
                    {
                        ContentTypeCode = contentType.Key.Code
                    },
                    null);

                // Move rules w/ priority >= to new rule's priority 1 value forward.
                foreach (Rule existentRule in existentRules.Where(r => r.Priority >= createRuleArgs.Priority))
                {
                    existentRule.Priority++;
                    await this.ruleRepository.Update(existentRule);
                }

                await this.ruleRepository.Add(rule);

                createRuleResult.CreatedRule = rule;
            }

            return createRuleResult;
        }

        private async Task<IConditionNode> CreateConditionNodeRecursive(Guid tenantId, CreateConditionNodeBase createConditionNodeBase, CreateRuleResult createRuleResult)
        {
            switch (createConditionNodeBase)
            {
                case CreateComposedConditionNode cccn:
                    LogicalOperatorCodes logicalOperatorCode = (LogicalOperatorCodes)cccn.LogicalOperatorCode;

                    if (logicalOperatorCode != LogicalOperatorCodes.And && logicalOperatorCode != LogicalOperatorCodes.Or)
                    {
                        createRuleResult
                            .AddErrorMessage("RXXX", $"Specified invalid logical operator code for condition node. (LogicalOperator = {logicalOperatorCode.ToString()})");
                    }

                    if (!cccn.ChildNodes.Any())
                    {
                        createRuleResult.AddErrorMessage("RXXX", "Specified empty collection of child nodes for condition node. Must have one child node at least.");
                    }

                    List<IConditionNode> conditionNodes = new List<IConditionNode>();
                    foreach (CreateConditionNodeBase ccnb in cccn.ChildNodes)
                    {
                        IConditionNode conditionNode = await this.CreateConditionNodeRecursive(tenantId, ccnb, createRuleResult);
                        conditionNodes.Add(conditionNode);
                    }

                    return this.conditionNodeAbstractFactory.CreateComposedConditionNode(logicalOperatorCode, conditionNodes);

                case CreateValueConditionNode cvcn:
                    ConditionTypeKey conditionTypeKey = ConditionTypeKey.New(tenantId, cvcn.ConditionTypeCode);
                    ConditionType conditionType = await this.conditionTypeRepository.GetById(conditionTypeKey);

                    if (conditionType == null)
                    {
                        createRuleResult.AddErrorMessage("RXXX", $"Specified invalid condition type for condition node. (TenantId = {tenantId} | ConditionTypeCode = {cvcn.ConditionTypeCode})");
                    }

                    DataTypeCodes dataTypeCode = (DataTypeCodes)cvcn.DataTypeCode;
                    if (!Enum.IsDefined(typeof(DataTypeCodes), dataTypeCode))
                    {
                        createRuleResult.AddErrorMessage("RXXX", $"Specified invalid data type for condition node. (DataTypeCode = {cvcn.DataTypeCode})");
                    }

                    OperatorCodes operatorCode = (OperatorCodes)cvcn.OperatorCode;
                    if (!Enum.IsDefined(typeof(OperatorCodes), operatorCode))
                    {
                        createRuleResult.AddErrorMessage("RXXX", $"Specified invalid operator for condition node. (OperatorCode = {cvcn.OperatorCode})");
                    }

                    if (Enum.IsDefined(typeof(DataTypeCodes), dataTypeCode) && !this.IsValidDataTypeValue(dataTypeCode, cvcn.RightHandOperand))
                    {
                        createRuleResult
                            .AddErrorMessage("RXXX", $"Specified invalid right hand operand value for specified data type. (DataType = {dataTypeCode.ToString()} | Value = {cvcn?.ToString()})");
                    }

                    return this.conditionNodeAbstractFactory.CreateValueConditionNode(conditionType, dataTypeCode, operatorCode, cvcn.RightHandOperand);

                default:
                    throw new NotSupportedException("Unsupported condition node creation.");
            }
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