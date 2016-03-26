namespace BoatRacingSimulator.Interfaces
{
    public interface IBoat : IModelable, IWeightable
    {
        double CalculateRaceSpeed(IRace race);
    }
}