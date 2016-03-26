namespace HotelBookingSystem.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        void Add(T item);

        bool Delete(int id);

        T Get(int id);

        IEnumerable<T> GetAll();

        bool Update(int id, T newItem);
    }
}