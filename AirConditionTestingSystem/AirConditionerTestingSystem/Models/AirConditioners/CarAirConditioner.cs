namespace AirConditionerTestingSystem.Models.AirConditioners
{
    using System;
    using System.Text;

    using Utilities;

    public class CarAirConditioner : AirConditioner
    {
        private int volumeCovered;

        public CarAirConditioner(string manufacturer, string model, int volumeCovered)
            : base(manufacturer, model)
        {
            this.VolumeCovered = volumeCovered;
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

        public override int Test()
        {
            double sqrtVolume = Math.Sqrt(this.volumeCovered);
            if (sqrtVolume < 3)
            {
                return 0;
            }

            return 1;
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append(base.ToString())
                .AppendFormat("Volume Covered: {0}", this.VolumeCovered)
                .AppendLine()
                .Append("====================");
            return toString.ToString();
        }
    }
}