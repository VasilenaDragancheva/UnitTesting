namespace BoatRacingSimulator.Models
{
    using System;

    using Interfaces;

    using Utility;

    public class Yacht : Boat, IYacht
    {
        private int cargoWeight;

        public Yacht(string model, int weight, IBoatEngine boatEngine, int cargoWeight)
            : base(model, weight)
        {
            this.BoatEngine = boatEngine;
            this.CargoWeight = cargoWeight;
        }

        public IBoatEngine BoatEngine { get; private set; }

        public int CargoWeight
        {
            get
            {
                return this.cargoWeight;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(Constants.IncorrectPropertyValueMessage, "Cargo Weight");
                }

                this.cargoWeight = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            var speed = this.BoatEngine.Output - (this.Weight + this.CargoWeight) + race.OceanCurrentSpeed;
            return speed;
        }
    }
}