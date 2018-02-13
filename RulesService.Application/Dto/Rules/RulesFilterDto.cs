using System;

namespace RulesService.Application.Dto.Rules
{
    public class RulesFilterDto
    {
        public int? ContentTypeCode { get; set; }

        public DateTime? FilterDateBegin { get; set; }

        public DateTime? FilterDateEnd { get; set; }
    }
}