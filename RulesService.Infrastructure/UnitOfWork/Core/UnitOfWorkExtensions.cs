namespace RulesService.Infrastructure.UnitOfWork.Core
{
    internal static class UnitOfWorkExtensions
    {
        internal static InternalState GetInternalState(this IUnitOfWork unitOfWork)
        {
            if (unitOfWork is IUnitOfWorkInternal unitOfWorkInternal)
            {
                return unitOfWorkInternal.InternalState;
            }

            return null;
        }
    }
}