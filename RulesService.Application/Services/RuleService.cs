using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.ConversionProfiles;
using RulesService.Application.Dto.Common;
using RulesService.Application.Dto.Rules;
using RulesService.Application.Exceptions;
using RulesService.Domain.Models;
using RulesService.Domain.Models.ConditionNodes;
using RulesService.Domain.Repositories;
using RulesService.Domain.Services.Rules;

namespace RulesService.Application.Services
{
    internal class RuleService : IRuleService
    {
        private readonly ICreateRuleConversionProfile createRuleConversionProfile;

        private readonly ICreateRuleService createRuleService;

        private readonly IRuleRepository ruleRepository;

        private readonly IUpdateRuleConversionProfile updateRuleConversionProfile;

        private readonly IUpdateRuleService updateRuleService;

        public RuleService(
            ICreateRuleConversionProfile createRuleConversionProfile,
            ICreateRuleService createRuleService,
            IRuleRepository ruleRepository,
            IUpdateRuleConversionProfile updateRuleConversionProfile,
            IUpdateRuleService updateRuleService)
        {
            this.createRuleConversionProfile = createRuleConversionProfile;
            this.createRuleService = createRuleService;
            this.ruleRepository = ruleRepository;
            this.updateRuleConversionProfile = updateRuleConversionProfile;
            this.updateRuleService = updateRuleService;
        }

        public async Task<RuleResultDto> Add(Guid tenantId, CreateRuleDto createRuleDto)
        {
            CreateRule createRule = this.createRuleConversionProfile.Convert(tenantId, createRuleDto);

            RuleResult ruleResult = await this.createRuleService.CreateRule(createRule);

            return new RuleResultDto
            {
                AffectedRule = ruleResult.AffectedRule != null ? this.ConvertToDto(ruleResult.AffectedRule) : null,
                ErrorMessages = ruleResult.ErrorMessages.Select(m => new { m.Code, m.Message })
            };
        }

        public async Task<IEnumerable<RuleDto>> GetAll(Guid tenantId, RulesFilterDto rulesFilterDto, PaginationDto paginationDto)
        {
            RulesFilter rulesFilter = rulesFilterDto?.ToRulesFilter();
            Pagination pagination = paginationDto?.ToPagination();

            return await this.ruleRepository.GetAll(tenantId, rulesFilter, pagination)
                .ContinueWith(rulesTask =>
                {
                    IEnumerable<Rule> rules = rulesTask.GetAwaiter().GetResult();

                    return rules.Select(r => this.ConvertToDto(r));
                });
        }

        public async Task<RuleDto> GetBy(Guid tenantId, Guid id)
        {
            RuleKey ruleKey = RuleKey.New(tenantId, id);
            return await this.ruleRepository.GetById(ruleKey)
                .ContinueWith(ruleTask =>
                {
                    Rule rule = ruleTask.GetAwaiter().GetResult();

                    if (rule != null)
                    {
                        return this.ConvertToDto(rule);
                    }

                    return null;
                });
        }

        public async Task<RuleResultDto> Update(Guid tenantId, Guid id, UpdateRuleDto updateRuleDto)
        {
            RuleKey ruleKey = RuleKey.New(tenantId, id);
            Rule rule = await this.ruleRepository.GetById(ruleKey);

            if (rule == null)
            {
                throw new NotFoundException(FormattableString.Invariant($"{nameof(Rule)} was not found. TenantId = {tenantId} | Id = {id}"));
            }

            UpdateRule updateRule = this.updateRuleConversionProfile.Convert(tenantId, id, updateRuleDto);

            RuleResult ruleResult = await this.updateRuleService.UpdateRule(updateRule);

            return new RuleResultDto
            {
                AffectedRule = ruleResult.AffectedRule != null ? this.ConvertToDto(ruleResult.AffectedRule) : null,
                ErrorMessages = ruleResult.ErrorMessages.Select(m => new { m.Code, m.Message })
            };
        }

        private ConditionNodeBaseDto ConvertNodeToDto(IConditionNode conditionNode)
        {
            switch (conditionNode)
            {
                case ComposedConditionNode ccn:
                    return new ComposedConditionNodeDto
                    {
                        ChildNodes = ccn.ChildNodes.Select(cn => this.ConvertNodeToDto(cn)),
                        LogicalOperatorCode = ccn.LogicalOperatorCode.AsInteger()
                    };

                case IValueConditionNode vcn:
                    return new ValueConditionNodeDto
                    {
                        ConditionTypeCode = vcn.ConditionTypeCode,
                        DataTypeCode = vcn.DataTypeCode.AsInteger(),
                        LogicalOperatorCode = vcn.LogicalOperatorCode.AsInteger(),
                        OperatorCode = vcn.OperatorCode.AsInteger(),
                        RightHandOperand = vcn.GetRightHandOperandAsObject()
                    };

                default:
                    throw new NotSupportedException("Condition node type is not supported.");
            }
        }

        private RuleDto ConvertToDto(Rule rule) => new RuleDto
        {
            ContentTypeCode = rule.ContentTypeCode,
            DateBegin = rule.DateBegin,
            DateEnd = rule.DateEnd,
            Id = rule.Key.Id,
            Name = rule.Name,
            Priority = rule.Priority,
            RootCondition = rule.RootCondition != null ? this.ConvertNodeToDto(rule.RootCondition) : null,
            TenantId = rule.Key.TenantId
        };
    }
}