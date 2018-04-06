using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal class StagingArea<T> : IStagingArea<T>
        where T : class
    {
        private readonly IDeepCopyService deepCopyService;

        private readonly List<StagedItem<T>> stagedItems;

        public StagingArea(IDeepCopyService deepCopyService)
        {
            this.deepCopyService = deepCopyService;
            this.stagedItems = new List<StagedItem<T>>(0);
        }

        public IEnumerable<StagedItem<T>> StagedItems => this.stagedItems.AsReadOnly();

        public T GetFromOriginal(T obj)
        {
            StagedItem<T> stagedItem = this.stagedItems.SingleOrDefault(i => object.Equals(i.Original, obj));
            if (stagedItem != default(StagedItem<T>))
            {
                return stagedItem.Staged;
            }

            return null;
        }

        public T GetFromStaged(T obj)
        {
            StagedItem<T> stagedItem = this.stagedItems.SingleOrDefault(i => object.Equals(i.Staged, obj));
            if (stagedItem != default(StagedItem<T>))
            {
                return stagedItem.Original;
            }

            return null;
        }

        public void Reset()
        {
            this.stagedItems.Clear();
        }

        public void Stage(T originalObj, T stagedObj, OperationCodes operationCode)
        {
            T copy = (T)this.deepCopyService.Copy(stagedObj);
            StagedItem<T> stagedItem = this.stagedItems.SingleOrDefault(i => object.Equals(i.Original, originalObj));

            if (stagedItem != default(StagedItem<T>))
            {
                stagedItem.Staged = copy;
            }
            else
            {
                stagedItem = new StagedItem<T>
                {
                    Operation = operationCode,
                    Original = originalObj,
                    Staged = copy
                };

                this.stagedItems.Add(stagedItem);
            }
        }

        public IDisposable Subscribe(IObserver<StagedItem<T>> observer)
        {
            throw new NotImplementedException();
        }
    }
}