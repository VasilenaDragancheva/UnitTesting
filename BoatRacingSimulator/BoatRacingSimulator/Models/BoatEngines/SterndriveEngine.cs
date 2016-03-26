namespace BoatRacingSimulator.Models
{
    public class SterndriveEngine : BoatEngine
    {
        private const int Multiplier = 7;

        public SterndriveEngine(string model, int horsepower, int displacement)
            : base(model, horsepower, displacement)
        {
        }

        public override int Output
        {
            get
            {
                return (this.Horsepower * Multiplier) + this.Displacement;
            }
        }
    }
}