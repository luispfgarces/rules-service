using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.Dto.DataTypes;
using RulesService.Domain.Models;

namespace RulesService.Application.Services
{
    internal class DataTypeService : IDataTypeService
    {
        private static IEnumerable<DataTypeCodes> DataTypeCodes => DataTypeService.FetchDataTypes();

        public async Task<IEnumerable<DataTypeDto>> GetAll()
        {
            return await Task.FromResult(DataTypeService.DataTypeCodes.Select(dtc => this.ConvertToDto(dtc)));
        }

        public async Task<DataTypeDto> GetBy(int code)
        {
            DataTypeCodes? dataTypeCode = DataTypeService.DataTypeCodes.SingleOrDefault(dtc => (int)dtc == code);

            if (dataTypeCode != null)
            {
                return await Task.FromResult(this.ConvertToDto(dataTypeCode.Value));
            }

            return await Task.FromException<DataTypeDto>(null);
        }

        private static IEnumerable<DataTypeCodes> FetchDataTypes()
        {
            List<DataTypeCodes> dataTypeCodesList = new List<DataTypeCodes>(0);
            foreach (object dataTypeCode in Enum.GetValues(typeof(DataTypeCodes)))
            {
                dataTypeCodesList.Add((DataTypeCodes)dataTypeCode);
            }
            return dataTypeCodesList;
        }

        private DataTypeDto ConvertToDto(DataTypeCodes dtc) => new DataTypeDto
        {
            Code = dtc.AsInteger(),
            Name = dtc.ToString()
        };
    }
}