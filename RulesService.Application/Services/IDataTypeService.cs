using System.Collections.Generic;
using System.Threading.Tasks;
using RulesService.Application.Dto.DataTypes;

namespace RulesService.Application.Services
{
    public interface IDataTypeService
    {
        Task<IEnumerable<DataTypeDto>> GetAll();

        Task<DataTypeDto> GetBy(int code);
    }
}