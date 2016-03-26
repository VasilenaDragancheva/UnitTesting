namespace BoatRacingSimulator.Models
{
    using System;

    using Interfaces;

    using Utility;

    public abstract class Boat : IBoat
    {
        private string model;

        private int weight;

        protected Boat(string model, int weight)
        {
            this.Model = model;
            this.Weight = weight;
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            private set
            {
                if (value.Length < Constants.MinBoatModelLength)
                {
                    throw new ArgumentException(
                        string.Format(Constants.IncorrectModelLenghtMessage, Constants.MinBoatModelLength));
                }

                this.model = value;
            }
        }

        public int Weight
        {
            get
            {
                return this.weight;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyValueMessage, "Weight"));
                }

                this.weight = value;
            }
        }

        public abstract double CalculateRaceSpeed(IRace race);
    }
}