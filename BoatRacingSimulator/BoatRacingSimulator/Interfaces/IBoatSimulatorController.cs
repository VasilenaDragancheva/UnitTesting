namespace BoatRacingSimulator.Interfaces
{
    public interface IBoatSimulatorController
    {
        IRace CurrentRace { get; }

        IBoatSimulatorDatabase Database { get; }

        string CreateBoatEngine(string model, int horsepower, int displacement, string engineType);

        string CreatePowerBoat(string model, int weight, string firstEngineModel, string secondEngineModel);

        string CreateRowBoat(string model, int weight, int oars);

        string CreateSailBoat(string model, int weight, int sailEfficiency);

        string CreateYacht(string model, int weight, string engineModel, int cargoWeight);

        string GetStatistic();

        string OpenRace(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats);

        string SignUpBoat(string model);

        string StartRace();
    }
}