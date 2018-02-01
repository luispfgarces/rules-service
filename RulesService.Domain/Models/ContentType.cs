using System;
using System.Collections.Generic;
using System.Text;

namespace RulesService.Domain.Model
{
    public class ContentType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Tenant Tenant { get; set; }
    }
}
