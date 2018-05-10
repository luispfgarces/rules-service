using Microsoft.Extensions.DependencyInjection;

namespace RulesService.Domain.Repositories
{
    public sealed class RepositoriesDependencyConfiguration
    {
        internal RepositoriesDependencyConfiguration(IServiceCollection serviceCollection)
        {
            this.ServiceCollection = serviceCollection;
        }

        internal IServiceCollection ServiceCollection { get; }

        public RepositoryDependencyConfiguration<TRepository> ForRepository<TRepository>()
            where TRepository : class => new RepositoryDependencyConfiguration<TRepository>(this);
    }
}