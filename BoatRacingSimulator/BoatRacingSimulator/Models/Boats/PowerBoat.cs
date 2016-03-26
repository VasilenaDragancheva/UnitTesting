namespace BoatRacingSimulator.Models
{
    using Interfaces;

    public class PowerBoat : Boat, IMotorBoat
    {
        public PowerBoat(string model, int weight, IBoatEngine boatEngine, IBoatEngine secondEngine)
            : base(model, weight)
        {
            this.BoatEngine = boatEngine;
            this.SecondEngine = secondEngine;
        }

        public IBoatEngine BoatEngine { get; private set; }

        public IBoatEngine SecondEngine { get; private set; }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = (this.BoatEngine.Output + this.SecondEngine.Output) - this.Weight
                        + ((double)race.OceanCurrentSpeed / 5);

            return speed;
        }
    }
}