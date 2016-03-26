namespace BoatRacingSimulator.Models
{
    using System;

    using Interfaces;

    using Utility;

    public abstract class BoatEngine : IBoatEngine
    {
        private string model;

        private int horsepower;

        private int displacement;

        protected BoatEngine(string model, int horsepower, int displacement)
        {
            this.Model = model;
            this.Horsepower = horsepower;
            this.Displacement = displacement;
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            private set
            {
                if (value.Length < Constants.MinBoatEngineModelLength)
                {
                    throw new ArgumentException(
                        string.Format(Constants.IncorrectModelLenghtMessage, Constants.MinBoatEngineModelLength));
                }

                this.model = value;
            }
        }

        public int Horsepower
        {
            get
            {
                return this.horsepower;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyValueMessage, "Horsepower"));
                }

                this.horsepower = value;
            }
        }

        public int Displacement
        {
            get
            {
                return this.displacement;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyValueMessage, "Displacement"));
                }

                this.displacement = value;
            }
        }

        public abstract int Output { get; }
    }
}