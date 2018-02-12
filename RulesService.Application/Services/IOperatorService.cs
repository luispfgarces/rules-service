using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Application.Dto.Operators;

namespace RulesService.Application.Services
{
    public interface IOperatorService
    {
        Task<IEnumerable<OperatorDto>> GetAll();

        Task<OperatorDto> GetByCode(int code);
    }
}