namespace RulesService.Infrastructure.UnitOfWork.Core.Messaging
{
    public class UnitOfWorkEvent
    {
        public string Code { get; set; }

        public UnitOfWorkCommandCodes Command { get; set; }

        public UnitOfWorkStateCodes State { get; set; }

        public object UnsubscriberKey => this.Code;
    }
}