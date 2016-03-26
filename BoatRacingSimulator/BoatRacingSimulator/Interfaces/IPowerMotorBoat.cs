namespace BoatRacingSimulator.Interfaces
{
    public interface IPowerMotorBoat : IMotorBoat
    {
        IBoatEngine SecondEngine { get; }
    }
}