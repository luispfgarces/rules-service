using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Domain.Models;
using RulesService.Domain.Models.ConditionNodes;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;
using RulesService.Domain.Services.Rules.Validation;

namespace RulesService.Domain.Services.Rules
{
    internal class CreateRuleService : ICreateRuleService
    {
        private readonly IConditionNodeAbstractFactory conditionNodeAbstractFactory;
        private readonly IConditionTypeRepository conditionTypeRepository;
        private readonly IContentTypeRepository contentTypeRepository;
        private readonly ICreateRuleValidator createRuleValidator;
        private readonly IRuleFactory ruleFactory;
        private readonly IRuleRepository ruleRepository;
        private readonly ITenantRepository tenantRepository;

        public CreateRuleService(
            ICreateRuleValidator createRuleValidator,
            IConditionNodeAbstractFactory conditionNodeAbstractFactory,
            IConditionTypeRepository conditionTypeRepository,
            IContentTypeRepository contentTypeRepository,
            IRuleFactory ruleFactory,
            IRuleRepository ruleRepository,
            ITenantRepository tenantRepository)
        {
            this.createRuleValidator = createRuleValidator;
            this.conditionNodeAbstractFactory = conditionNodeAbstractFactory;
            this.conditionTypeRepository = conditionTypeRepository;
            this.contentTypeRepository = contentTypeRepository;
            this.ruleFactory = ruleFactory;
            this.ruleRepository = ruleRepository;
            this.tenantRepository = tenantRepository;
        }

        public async Task<CreateRuleResult> CreateRule(CreateRule createRule)
        {
            CreateRuleResult createRuleResult = new CreateRuleResult();

            IEnumerable<CreateRuleValidationMessage> createRuleValidationMessages = this.createRuleValidator.Validate(createRule);

            foreach (CreateRuleValidationMessage validationMessage in createRuleValidationMessages)
            {
                createRuleResult.AddErrorMessage(validationMessage.Code, validationMessage.Message);
            }

            if (!createRuleResult.HasErrors)
            {
                // Fetch content type and validate.
                ContentTypeKey contentTypeKey = ContentTypeKey.New(createRule.TenantId, createRule.ContentTypeCode);
                ContentType contentType = await this.contentTypeRepository.GetById(contentTypeKey);

                IConditionNode rootCondition = null;
                if (createRule.RootCondition != null)
                {
                    rootCondition = await this.CreateConditionNodeRecursive(createRule.TenantId, createRule.RootCondition, createRuleResult);
                }

                Rule rule = this.ruleFactory.CreateRule(
                        createRule.TenantId,
                        createRule.Name,
                        contentType,
                        createRule.DateBegin,
                        createRule.DateEnd,
                        createRule.Priority,
                        rootCondition);

                IEnumerable<Rule> existentRules = await this.ruleRepository.GetAll(
                    createRule.TenantId,
                    new RulesFilter
                    {
                        ContentTypeCode = contentType.Key.Code
                    },
                    null);

                // Move rules w/ priority >= to new rule's priority 1 value forward.
                foreach (Rule existentRule in existentRules.Where(r => r.Priority >= createRule.Priority))
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
                    DataTypeCodes dataTypeCode = (DataTypeCodes)cvcn.DataTypeCode;
                    OperatorCodes operatorCode = (OperatorCodes)cvcn.OperatorCode;

                    return this.conditionNodeAbstractFactory.CreateValueConditionNode(conditionType, dataTypeCode, operatorCode, cvcn.RightHandOperand);

                default:
                    throw new NotSupportedException("Unsupported condition node creation.");
            }
        }
    }
}