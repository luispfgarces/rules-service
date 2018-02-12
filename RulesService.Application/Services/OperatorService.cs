using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.Dto.Operators;
using RulesService.Domain.Models;
using RulesService.Domain.Models.Operators;
using RulesService.Domain.Repositories;

namespace RulesService.Application.Services
{
    public class OperatorService : IOperatorService
    {
        private readonly IOperatorRepository operatorRepository;

        public OperatorService(IOperatorRepository operatorRepository)
        {
            this.operatorRepository = operatorRepository;
        }

        public async Task<IEnumerable<OperatorDto>> GetAll()
        {
            return await this.operatorRepository.GetAllOperators()
                .ContinueWith(operatorsTask =>
                {
                    IEnumerable<Operator> operators = operatorsTask.GetAwaiter().GetResult();

                    return operators.Select(o => this.ConvertToDto(o));
                });
        }

        public async Task<OperatorDto> GetByCode(int code)
        {
            OperatorCodes operatorCodes = (OperatorCodes)code;
            return await this.operatorRepository.GetOperator(operatorCodes)
                .ContinueWith(operatorTask =>
                {
                    Operator @operator = operatorTask.GetAwaiter().GetResult();

                    if (@operator != null)
                    {
                        return this.ConvertToDto(@operator);
                    }

                    return null;
                });
        }

        private OperatorDto ConvertToDto(Operator @operator) => new OperatorDto
        {
            Code = @operator.Code,
            Description = @operator.Description,
            Name = @operator.Name,
            Symbols = @operator.Symbols
        };
    }
}