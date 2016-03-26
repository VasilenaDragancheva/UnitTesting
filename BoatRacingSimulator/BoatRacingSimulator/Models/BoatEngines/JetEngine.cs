namespace BoatRacingSimulator.Models
{
    public class JetEngine : BoatEngine
    {
        private const int Multiplier = 5;

        public JetEngine(string model, int horsepower, int displacement)
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