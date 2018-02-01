using System;
using System.Collections.Generic;
using System.Text;
using RulesService.Domain.Core;

namespace RulesService.Domain.Model
{
    public class Tenant : EntityBase<Guid>
    {
        public Tenant()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        public string Name { get; set; }
    }
}
