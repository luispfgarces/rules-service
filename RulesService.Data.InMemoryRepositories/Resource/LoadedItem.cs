using System.Collections.Generic;

namespace RulesService.Data.InMemoryRepositories.Resource
{
    internal struct LoadedItem<T>
    {
        public T Loaded { get; set; }

        public T Persistent { get; set; }

        public static bool operator !=(LoadedItem<T> item1, LoadedItem<T> item2)
        {
            return !(item1 == item2);
        }

        public static bool operator ==(LoadedItem<T> item1, LoadedItem<T> item2)
        {
            return item1.Equals(item2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LoadedItem<T>))
            {
                return false;
            }

            var item = (LoadedItem<T>)obj;
            return EqualityComparer<T>.Default.Equals(Loaded, item.Loaded) &&
                   EqualityComparer<T>.Default.Equals(Persistent, item.Persistent);
        }

        public override int GetHashCode()
        {
            var hashCode = -2084435456;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Loaded);
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Persistent);
            return hashCode;
        }
    }
}