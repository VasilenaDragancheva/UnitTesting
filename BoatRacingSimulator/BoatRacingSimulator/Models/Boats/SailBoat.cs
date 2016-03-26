namespace BoatRacingSimulator.Models
{
    using System;

    using Interfaces;

    using Utility;

    public class SailBoat : Boat, ISailBoat
    {
        private int sailEfficiency;

        public SailBoat(string model, int weight, int sailEfficiency)
            : base(model, weight)
        {
            this.SailEfficiency = sailEfficiency;
        }

        public int SailEfficiency
        {
            get
            {
                return this.sailEfficiency;
            }

            private set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentException(Constants.IncorrectSailEfficiencyMessage);
                }

                this.sailEfficiency = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            double windSpeed = race.WindSpeed * ((double)this.SailEfficiency / 100);

            var speed = windSpeed - this.Weight + ((double)race.OceanCurrentSpeed / 2);
            return speed;
        }
    }
}