using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RulesService.Data.InMemoryRepositories;

namespace RulesService.Domain.Repositories
{
    public static class RepositoryDependencyConfigurationExtension
    {
        private static readonly Dictionary<string, Type> implementationsByInterfaceFullName = new Dictionary<string, Type>
        {
            { typeof(ITenantRepository).FullName, typeof(TenantInMemoryRepository) },
            { typeof(IConditionTypeRepository).FullName, typeof(ConditionTypeInMemoryRepository) },
            { typeof(IContentTypeRepository).FullName, typeof(ContentTypeInMemoryRepository) },
            { typeof(IOperatorRepository).FullName, typeof(OperatorInMemoryCachedRepository) },
            { typeof(IRuleRepository).FullName, typeof(RuleInMemoryRepository) }
        };

        public static void UseInMemory<TRepository>(this RepositoryDependencyConfiguration<TRepository> repositoryDependencyConfiguration)
            where TRepository : class
        {
            if (!implementationsByInterfaceFullName.ContainsKey(repositoryDependencyConfiguration.RepositoryType.FullName))
            {
                throw new NotSupportedException(
                    $"Provided repository interface type is not supported or it is not a repository interface type: {repositoryDependencyConfiguration.RepositoryType.FullName}.");
            }

            Type repositoryImplementationType = implementationsByInterfaceFullName[repositoryDependencyConfiguration.RepositoryType.FullName];
            repositoryDependencyConfiguration.ServiceCollection.AddSingleton(repositoryDependencyConfiguration.RepositoryType, repositoryImplementationType);
        }
    }
}