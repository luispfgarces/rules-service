using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.Dto.Common;
using RulesService.Application.Dto.Rules;
using RulesService.Domain.Models;
using RulesService.Domain.Models.ConditionNodes;
using RulesService.Domain.Repositories;

namespace RulesService.Application.Services
{
    internal class RuleService : IRuleService
    {
        private readonly IRuleRepository ruleRepository;

        public RuleService(IRuleRepository ruleRepository)
        {
            this.ruleRepository = ruleRepository;
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
            ContentTypeCode = rule.ContentType.Key.Code,
            DateBegin = rule.DateBegin,
            DateEnd = rule.DateEnd,
            Id = rule.Key.Id,
            Name = rule.Name,
            Priority = rule.Priority,
            RootCondition = this.ConvertNodeToDto(rule.RootCondition),
            TenantId = rule.Key.TenantId
        };
    }
}