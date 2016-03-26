namespace BoatRacingSimulator.Database
{
    using Interfaces;

    public class BoatSimulatorDatabase : IBoatSimulatorDatabase
    {
        public BoatSimulatorDatabase()
        {
            this.Boats = new Repository<IBoat>();
            this.Engines = new Repository<IBoatEngine>();
        }

        public IRepository<IBoat> Boats { get; private set; }

        public IRepository<IBoatEngine> Engines { get; private set; }
    }
}