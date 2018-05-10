using System;
using Microsoft.Extensions.DependencyInjection;

namespace RulesService.Domain.Repositories
{
    public class RepositoryDependencyConfiguration<TRepository>
        where TRepository : class
    {
        public RepositoryDependencyConfiguration(RepositoriesDependencyConfiguration repositoriesDependencyConfiguration)
        {
            this.RepositoryType = typeof(TRepository);
            this.ServiceCollection = repositoriesDependencyConfiguration.ServiceCollection;
        }

        public Type RepositoryType { get; }

        public IServiceCollection ServiceCollection { get; }
    }
}