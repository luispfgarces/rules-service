using System.Collections.Generic;
using System.Linq;

namespace RulesService.Infrastructure.UnitOfWork.Core
{
    internal class TransactionalState
    {
        private readonly List<Vote> votes;

        private bool completed;

        public TransactionalState()
        {
            this.completed = false;
            this.votes = new List<Vote>(0);
        }

        public bool Committable => this.completed && this.votes.Select(v => v.VotedOk).Aggregate((v1, v2) => v1 == v2);

        public bool Completed => this.completed;

        public IEnumerable<Vote> Votes => this.votes.AsReadOnly();

        public void SetCompleted() => this.completed = true;

        public void Vote(string unitOfWorkCode, bool isOk)
        {
            this.votes.Add(new Vote
            {
                UnitOfWorkCode = unitOfWorkCode,
                VotedOk = isOk
            });
        }
    }
}