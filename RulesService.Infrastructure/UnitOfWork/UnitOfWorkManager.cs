using System.Collections.Generic;
using RulesService.Infrastructure.UnitOfWork.Core;

namespace RulesService.Infrastructure.UnitOfWork
{
    internal class UnitOfWorkManager : IUnitOfWorkManager
    {
        private const string UnitOfWorkDefaultName = "unnamed-uow";

        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        private readonly Stack<IUnitOfWork> unitsOfWorkStack;

        public UnitOfWorkManager(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitsOfWorkStack = new Stack<IUnitOfWork>();
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public IUnitOfWork BeginUnitOfWork()
        {
            return this.BeginUnitOfWork(UnitOfWorkManager.UnitOfWorkDefaultName);
        }

        public IUnitOfWork BeginUnitOfWork(string name)
        {
            if (this.unitsOfWorkStack.Count == 0)
            {
                IMasterUnitOfWork masterUnitOfWork = this.unitOfWorkFactory.CreateMaster(name);
                this.unitsOfWorkStack.Push(masterUnitOfWork);
                return masterUnitOfWork;
            }

            IUnitOfWork parentUnitOfWork = this.unitsOfWorkStack.Peek();

            ISlaveUnitOfWork slaveUnitOfWork = this.unitOfWorkFactory.CreateSlave(name);
            InternalState slaveInternalState = slaveUnitOfWork.GetInternalState();
            InternalState parentInternalState = parentUnitOfWork.GetInternalState();
            parentInternalState.EventSubscriptionHandler.Subscribe(slaveInternalState.EventDispatcher);

            return slaveUnitOfWork;
        }
    }
}