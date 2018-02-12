using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Domain.Models;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Models.Operators;
using RulesService.Domain.Repositories;

namespace RulesService.Data.InMemoryRepositories
{
    internal class InMemoryCachedOperatorRepository : IOperatorRepository
    {
        private static readonly Type operatorCodesType = typeof(OperatorCodes);

        private readonly IOperatorFactory operatorFactory;

        private readonly ConcurrentDictionary<OperatorCodes, Operator> operatorsDictionary;

        private bool allOperatorsInitialized;

        public InMemoryCachedOperatorRepository(IOperatorFactory operatorFactory)
        {
            this.allOperatorsInitialized = false;
            this.operatorsDictionary = new ConcurrentDictionary<OperatorCodes, Operator>();
            this.operatorFactory = operatorFactory;
        }

        public async Task<IEnumerable<Operator>> GetAllOperators()
        {
            if (!allOperatorsInitialized)
            {
                this.EnsureAllOperatorsInitialized();
            }

            return await Task.FromResult<IEnumerable<Operator>>(this.operatorsDictionary.Values.ToList());
        }

        public async Task<Operator> GetOperator(OperatorCodes operatorCode)
        {
            Operator @operator = null;

            if (Enum.IsDefined(operatorCodesType, operatorCode))
            {
                @operator = this.operatorsDictionary.GetOrAdd(operatorCode, oc => this.operatorFactory.CreateOperator(oc));
            }

            return await Task.FromResult(@operator);
        }

        private void EnsureAllOperatorsInitialized()
        {
            Array operatorsArray = Enum.GetValues(typeof(OperatorCodes));
            foreach (object code in operatorsArray)
            {
                OperatorCodes operatorCode = (OperatorCodes)code;
                Operator @operator = this.operatorFactory.CreateOperator(operatorCode);
                this.operatorsDictionary.TryAdd(operatorCode, @operator);
            }

            this.allOperatorsInitialized = true;
        }
    }
}