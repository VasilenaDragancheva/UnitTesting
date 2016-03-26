namespace BoatRacingSimulator.Interfaces
{
    public interface IYacht : IMotorBoat
    {
        int CargoWeight { get; }
    }
}