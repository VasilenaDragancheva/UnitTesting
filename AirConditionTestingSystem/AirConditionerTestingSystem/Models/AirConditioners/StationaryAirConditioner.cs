namespace AirConditionerTestingSystem.Models.AirConditioners
{
    using System;
    using System.Text;

    using Utilities;

    public class StationaryAirConditioner : AirConditioner
    {
        private int powerUsage;

        private string requieredEfficiencyRating;

        public StationaryAirConditioner(
            string manufacturer, 
            string model, 
            string requieredEfficiencyRating, 
            int powerUsage)
            : base(manufacturer, model)
        {
            this.PowerUsage = powerUsage;
            this.RequieredEfficiencyRating = requieredEfficiencyRating;
        }

        public int PowerUsage
        {
            get
            {
                return this.powerUsage;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Power Usage"));
                }

                this.powerUsage = value;
            }
        }

        public string RequieredEfficiencyRating
        {
            get
            {
                return this.requieredEfficiencyRating;
            }

            private set
            {
                if (char.Parse(value) > 'E' || char.Parse(value) < 'A')
                {
                    throw new ArgumentException(Constants.IncorrectRating);
                }

                this.requieredEfficiencyRating = value;
            }
        }

        public override int Test()
        {
            var rating =
                (EnergyEfficiencyRating)Enum.Parse(typeof(EnergyEfficiencyRating), this.RequieredEfficiencyRating);
            var requiredMaxPowerUsage = (int)rating;
            if (this.PowerUsage > requiredMaxPowerUsage)
            {
                return 0;
            }

            return 1;
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append(base.ToString())
                .AppendFormat("Required energy efficiency rating: {0}", this.RequieredEfficiencyRating)
                .AppendLine()
                .AppendFormat("Power Usage(KW / h): {0}", this.PowerUsage)
                .AppendLine()
                .Append("====================");

            return toString.ToString();
        }
    }
}