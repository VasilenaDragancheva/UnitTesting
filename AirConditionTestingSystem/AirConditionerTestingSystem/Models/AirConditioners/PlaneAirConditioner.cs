namespace AirConditionerTestingSystem.Models.AirConditioners
{
    using System;
    using System.Text;

    using Utilities;

    public class PlaneAirConditioner : AirConditioner
    {
        private int volumeCovered;

        private int electricityUsed;

        public PlaneAirConditioner(string manufacturer, string model, int volumeCovered, int electricityUsed)
            : base(manufacturer, model)
        {
            this.VolumeCovered = volumeCovered;
            this.ElectricityUsed = electricityUsed;
        }

        public int VolumeCovered
        {
            get
            {
                return this.volumeCovered;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Volume Covered"));
                }

                this.volumeCovered = value;
            }
        }

        public int ElectricityUsed
        {
            get
            {
                return this.electricityUsed;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Electricity Used"));
                }

                this.electricityUsed = value;
            }
        }

        public override int Test()
        {
            double sqrtVolume = Math.Sqrt(this.volumeCovered);
            double result = this.ElectricityUsed / sqrtVolume;
            if (result < 150)
            {
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append(base.ToString())
                .AppendFormat("Volume Covered:  {0}", this.VolumeCovered)
                .AppendLine()
                .AppendFormat("Electricity Used: {0}", this.ElectricityUsed)
                .AppendLine()
                .Append("====================");
            return toString.ToString();
        }
    }
}