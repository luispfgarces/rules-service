namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    public enum UnitOfWorkStateCodes
    {
        PreBegin = 1,

        PostBegin = 2,

        PreRollback = 3,

        PostRollback = 4,

        PreDoComplete = 5,

        PostDoComplete = 6,

        PreFinalize = 7,

        PostFinalize = 8,

        PreCommit = 9,

        PostCommit = 10
    }
}