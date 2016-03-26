namespace BoatRacingSimulator.Interfaces
{
    public interface IBoatEngine : IModelable
    {
        int Horsepower { get; }

        int Displacement { get; }

        int Output { get; }
    }
}