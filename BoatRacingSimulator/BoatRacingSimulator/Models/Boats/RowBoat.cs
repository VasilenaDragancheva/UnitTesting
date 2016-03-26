namespace BoatRacingSimulator.Models
{
    using System;

    using Interfaces;

    using Utility;

    public class RowBoat : Boat, IRowBoat
    {
        private int oars;

        public RowBoat(string model, int weight, int oars)
            : base(model, weight)
        {
            this.Oars = oars;
        }

        public int Oars
        {
            get
            {
                return this.oars;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyValueMessage, "Oars"));
                }

                this.oars = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = (this.Oars * 100) - this.Weight + race.OceanCurrentSpeed;
            return speed;
        }
    }
}