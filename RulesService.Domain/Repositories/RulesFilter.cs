using System;

namespace RulesService.Domain.Repositories
{
    public class RulesFilter
    {
        public int? ContentTypeCode { get; set; }

        public DateTime? FilterDateBegin { get; set; }

        public DateTime? FilterDateEnd { get; set; }
    }
}