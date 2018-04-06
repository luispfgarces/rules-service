namespace RulesService.Infrastructure.UnitOfWork
{
    internal class SlaveUnitOfWork : UnitOfWorkTemplate, ISlaveUnitOfWork
    {
        public SlaveUnitOfWork(string name)
            : base(name)
        {
        }

        public override UnitOfWorkTypeCodes TypeCode => UnitOfWorkTypeCodes.Slave;

        protected override void Commit()
        {
            // Slave implementation does not perform any actual action.
        }

        protected override void Rollback()
        {
            // Slave implementation does not perform any actual action.
        }
    }
}