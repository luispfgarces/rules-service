using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Domain.Models;
using RulesService.Domain.Models.Operators;

namespace RulesService.Domain.Repositories
{
    public interface IOperatorRepository
    {
        Task<IEnumerable<Operator>> GetAllOperators();

        Task<Operator> GetOperator(OperatorCodes operatorCode);
    }
}