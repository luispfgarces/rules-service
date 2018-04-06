using System.Collections.Generic;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal struct StagedItem<T>
    {
        public OperationCodes Operation { get; set; }

        public T Original { get; set; }

        public T Staged { get; set; }

        public static bool operator !=(StagedItem<T> item1, StagedItem<T> item2)
        {
            return !(item1 == item2);
        }

        public static bool operator ==(StagedItem<T> item1, StagedItem<T> item2)
        {
            return item1.Equals(item2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StagedItem<T>))
            {
                return false;
            }

            var item = (StagedItem<T>)obj;
            return Operation == item.Operation &&
                   EqualityComparer<T>.Default.Equals(Original, item.Original) &&
                   EqualityComparer<T>.Default.Equals(Staged, item.Staged);
        }

        public override int GetHashCode()
        {
            var hashCode = 846910067;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Operation.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Original);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Staged);
            return hashCode;
        }
    }
}