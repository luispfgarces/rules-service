using System;
using RulesService.Infrastructure.UnitOfWork.Resources;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal class InMemoryResource : IResource
    {
        public IResourceTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}