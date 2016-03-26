namespace HotelBookingSystem.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public class Repository<T> : IRepository<T>
        where T : IDbEntity
    {
        private readonly Dictionary<int, T> items;

        private int nextAddId = 1;

        public Repository()
        {
            this.items = new Dictionary<int, T>(new DbEqualityComparer());
        }

        public virtual void Add(T item)
        {
            item.Id = this.nextAddId;
            this.items.Add(this.nextAddId, item);
            this.nextAddId++;
        }

        public virtual bool Delete(int id)
        {
            return this.items.Remove(id);
        }

        public virtual T Get(int id)
        {
            T item;
            try
            {
                item = this.items[id];
            }
            catch (KeyNotFoundException)
            {
                item = default(T);
            }

            return item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.items.OrderBy(item => item.Key).Select(item => item.Value);
        }

        public virtual bool Update(int id, T newItem)
        {
            var item = this.Get(id);
            if (item == null)
            {
                return false;
            }

            item = newItem;
            return true;
        }

        private class DbEqualityComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public int GetHashCode(int obj)
            {
                return obj % 10;
            }
        }
    }
}