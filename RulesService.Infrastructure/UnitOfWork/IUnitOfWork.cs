using System;

namespace RulesService.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        string Code { get; }

        UnitOfWorkTypeCodes TypeCode { get; }

        void Complete();
    }
}