namespace RulesService.Infrastructure.UnitOfWork.Core
{
    internal class Vote
    {
        public string UnitOfWorkCode { get; set; }

        public bool VotedOk { get; set; }
    }
}