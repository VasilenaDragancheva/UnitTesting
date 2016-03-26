namespace AirConditionerTestingSystem.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Models;
    using Models.AirConditioners;

    public class AirConditionerSystemData
    {
        public AirConditionerSystemData()
        {
            this.AirConditioners = new List<AirConditioner>();
            this.Reports = new List<Report>();
        }

        public List<Report> Reports { get; set; }

        public List<AirConditioner> AirConditioners { get; set; }

        public void AddAirConditioner(AirConditioner airConditioner)
        {
            this.AirConditioners.Add(airConditioner);
        }

        public void AddReport(Report report)
        {
            this.Reports.Add(report);
        }

        public AirConditioner GetAirConditioner(string manufacturer, string model)
        {
            return this.AirConditioners.Where(x => x.Manufacturer == manufacturer && x.Model == model).FirstOrDefault();
        }

        public int GetAirConditionersCount()
        {
            return this.AirConditioners.Count;
        }

        public Report GetReport(string manufacturer, string model)
        {
            return this.Reports.Where(x => x.Manufacturer == manufacturer && x.Model == model).FirstOrDefault();
        }

        public List<Report> GetReportsByManufacturer(string manufacturer)
        {
            return this.Reports.Where(x => x.Manufacturer == manufacturer).ToList();
        }

        public int GetReportsCount()
        {
            return this.Reports.Count;
        }

        public void RemoveAirConditioner(AirConditioner airConditioner)
        {
            this.AirConditioners.Remove(airConditioner);
        }

        public void RemoveReport(Report report)
        {
            this.Reports.Remove(report);
        }
    }
}