namespace AirConditionerTestingSystem.Models
{
    using System.Text;

    public class Report
    {
        public Report(string manufacturer, string model, int mark)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Mark = mark;
        }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int Mark { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine("Report")
                .AppendLine("====================")
                .AppendFormat("Manufacturer: {0}", this.Manufacturer)
                .AppendLine()
                .AppendFormat("Model: {0}", this.Model)
                .AppendLine()
                .AppendFormat("Mark: {0}", this.Mark == 1 ? "Passed" : "Failed")
                .AppendLine()
                .Append("====================");

            return result.ToString();
        }
    }
}