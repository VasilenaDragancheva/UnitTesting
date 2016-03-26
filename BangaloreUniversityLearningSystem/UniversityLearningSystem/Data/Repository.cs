namespace UniversityLearningSystem.Data
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class Repository<T> : IRepository<T>
    {
        private readonly List<T> items;

        public Repository()
        {
            this.items = new List<T>();
        }

        public virtual void Add(T item)
        {
            this.items.Add(item);
        }

        public virtual T Get(int id)
        {
            T item;
            try
            {
                item = this.items[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                item = default(T);
            }

            return item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.items;
        }
    }
}