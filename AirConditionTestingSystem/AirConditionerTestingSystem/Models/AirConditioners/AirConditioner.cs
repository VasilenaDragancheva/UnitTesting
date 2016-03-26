namespace AirConditionerTestingSystem.Models.AirConditioners
{
    using System;
    using System.Text;

    using Contracts;

    using Utilities;

    public abstract class AirConditioner : IAirConditioner
    {
        private string manufacturer;

        private string model;

        protected AirConditioner(string manufacturer, string model)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
        }

        public string Manufacturer
        {
            get
            {
                return this.manufacturer;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < Constants.ManufacturerMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyLength, "Manufacturer", 4));
                }

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < Constants.ModelMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyLength, "Model", 2));
                }

                this.model = value;
            }
        }

        public abstract int Test();

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.AppendLine("Air Conditioner")
                .AppendLine("====================")
                .AppendFormat("Manufacturer: {0}", this.Manufacturer)
                .AppendLine()
                .AppendFormat("Model: {0}", this.Model)
                .AppendLine();
            return toString.ToString();
        }
    }
}