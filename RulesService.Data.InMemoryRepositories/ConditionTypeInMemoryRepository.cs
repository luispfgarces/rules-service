using System;
using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Data.InMemoryRepositories
{
    internal class ConditionTypeInMemoryRepository : InMemoryRepositoryBase<ConditionType, ConditionTypeKey>, IConditionTypeRepository
    {
        public ConditionTypeInMemoryRepository()
        {
            this.entities.AddRange(new[]
            {
                new ConditionType(Guid.Parse("d29c8e8b-0e46-4993-9c77-25e48bdd6691"), 1, DataTypeCodes.Decimal)
                {
                    Name = "VAT Rate",
                    Description = "Constrains a rule by VAT rate."
                },
                new ConditionType(Guid.Parse("2999babb-16ea-446c-84d5-d08093377e48"), 1, DataTypeCodes.String)
                {
                    Name = "ISO Currency",
                    Description = "Constrains a rule by ISO currency codes."
                },
                new ConditionType(Guid.Parse("2999babb-16ea-446c-84d5-d08093377e48"), 2, DataTypeCodes.Integer)
                {
                    Name = "Rating",
                    Description = "Constrains by rating (1 to 5)."
                }
            });
        }
    }
}