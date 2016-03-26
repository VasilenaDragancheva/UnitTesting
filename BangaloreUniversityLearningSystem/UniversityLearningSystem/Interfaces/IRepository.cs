namespace UniversityLearningSystem.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        void Add(T item);

        T Get(int id);

        IEnumerable<T> GetAll();
    }
}