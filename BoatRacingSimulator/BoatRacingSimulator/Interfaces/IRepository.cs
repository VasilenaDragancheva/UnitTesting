namespace BoatRacingSimulator.Interfaces
{
    public interface IRepository<T>
        where T : IModelable
    {
        void Add(T item);

        T GetItem(string model);
    }
}