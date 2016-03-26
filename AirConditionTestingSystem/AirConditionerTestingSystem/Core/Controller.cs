namespace AirConditionerTestingSystem.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CustomExceptions;

    using Data;

    using Models;
    using Models.AirConditioners;

    using Utilities;

    public class Controller
    {
        public Controller()
        {
            this.AirConditionerSystemDatabase = new AirConditionerSystemData();
        }

        public AirConditionerSystemData AirConditionerSystemDatabase { get; private set; }

        public virtual string ExecuteCommand(Command command)
        {
            string commandResult = string.Empty;
            switch (command.Name)
            {
                case "RegisterStationaryAirConditioner":
                    this.ValidateParametersCount(command, 4);
                    commandResult = this.RegisterStationaryAirConditioner(
                        command.Parameters[0], 
                        command.Parameters[1], 
                        command.Parameters[2], 
                        int.Parse(command.Parameters[3]));
                    break;
                case "RegisterCarAirConditioner":
                    this.ValidateParametersCount(command, 3);
                    commandResult = this.RegisterCarAirConditioner(
                        command.Parameters[0], 
                        command.Parameters[1], 
                        int.Parse(command.Parameters[2]));
                    break;
                case "RegisterPlaneAirConditioner":
                    this.ValidateParametersCount(command, 4);
                    commandResult = this.RegisterPlaneAirConditioner(
                        command.Parameters[0], 
                        command.Parameters[1], 
                        int.Parse(command.Parameters[2]), 
                        int.Parse(command.Parameters[3]));
                    break;
                case "TestAirConditioner":
                    this.ValidateParametersCount(command, 2);
                    commandResult = this.TestAirConditioner(command.Parameters[0], command.Parameters[1]);
                    break;
                case "FindAirConditioner":
                    this.ValidateParametersCount(command, 2);
                    commandResult = this.FindAirConditioner(command.Parameters[0], command.Parameters[1]);
                    break;
                case "FindReport":
                    this.ValidateParametersCount(command, 2);
                    commandResult = this.FindReport(command.Parameters[0], command.Parameters[1]);
                    break;
                case "Status":
                    this.ValidateParametersCount(command, 0);
                    commandResult = this.Status();
                    break;
                case "FindAllReportsByManufacturer":
                    this.ValidateParametersCount(command, 1);
                    commandResult = this.FindAllReportsByManufacturer(command.Parameters[0]);
                    break;
                default:
                    throw new InvalidOperationException(Constants.InvalidCommand);
            }

            return commandResult;
        }

        public void ExecuteCommand()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds already registered aircodinitioner by manifacture and model
        /// </summary>
        /// <param name="manufacturer">manifacture of conditioner</param>
        /// <param name="model"> model of conditioner</param>
        /// <returns>If is found returns specifications or not found</returns>
        private string FindAirConditioner(string manufacturer, string model)
        {
            AirConditioner airConditioner = this.AirConditionerSystemDatabase.GetAirConditioner(manufacturer, model);
            if (airConditioner == null)
            {
                throw new NonExistantEntryException(Constants.NotExistingEntry);
            }

            return airConditioner.ToString();
        }

        private string FindAllReportsByManufacturer(string manufacturer)
        {
            List<Report> reports = this.AirConditionerSystemDatabase.GetReportsByManufacturer(manufacturer);
            if (reports.Count == 0)
            {
                return Constants.NoReports;
            }

            reports = reports.OrderBy(x => x.Mark).ToList();
            StringBuilder reportsPrint = new StringBuilder();
            reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
            reportsPrint.Append(string.Join(Environment.NewLine, reports));
            return reportsPrint.ToString();
        }

        private string FindReport(string manufacturer, string model)
        {
            Report report = this.AirConditionerSystemDatabase.GetReport(manufacturer, model);
            if (report == null)
            {
                throw new NonExistantEntryException(Constants.NotExistingEntry);
            }

            return report.ToString();
        }

        private string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage)
        {
            CarAirConditioner airConditioner = new CarAirConditioner(manufacturer, model, volumeCoverage);
            return this.RegisterAirConditioner(airConditioner);
        }

        /// <summary>
        /// Registers plain aircodnitioners
        /// </summary>
        /// <param name="manufacturer">manifecture of conditioner</param>
        /// <param name="model">model of conditioner</param>
        /// <param name="volumeCoverage"> volue coverage </param>
        /// <param name="electricityUsed"> used electricity</param>
        /// <returns>String whether the registrations was successful or not</returns>
        private string RegisterPlaneAirConditioner(
            string manufacturer, 
            string model, 
            int volumeCoverage, 
            int electricityUsed)
        {
            var airConditioner = new PlaneAirConditioner(manufacturer, model, volumeCoverage, electricityUsed);

            return this.RegisterAirConditioner(airConditioner);
        }

        private string RegisterAirConditioner(AirConditioner airConditioner)
        {
            var existing =
                this.AirConditionerSystemDatabase.AirConditioners.FirstOrDefault(
                    ac => ac.Manufacturer == airConditioner.Manufacturer && ac.Model == airConditioner.Model);
            if (existing != null)
            {
                throw new DuplicateEntryException(Constants.DuplicateEntry);
            }

            this.AirConditionerSystemDatabase.AirConditioners.Add(airConditioner);
            var result = string.Format(Constants.RegisterConditioner, airConditioner.Model, airConditioner.Manufacturer);
            return result;
        }

        private string RegisterStationaryAirConditioner(
            string manufacturer, 
            string model, 
            string energyEfficiencyRating, 
            int powerUsage)
        {
            var airConditioner = new StationaryAirConditioner(manufacturer, model, energyEfficiencyRating, powerUsage);
            return this.RegisterAirConditioner(airConditioner);
        }

        /// <summary>
        /// Calculates how much of registered airconditionrs are tested
        /// </summary>
        /// <returns>String for percentage of tested airconditioners</returns>
        private string Status()
        {
            int reports = this.AirConditionerSystemDatabase.GetReportsCount();
            double airConditioners = this.AirConditionerSystemDatabase.GetAirConditionersCount();

            double percent = reports / airConditioners;
            percent = percent * 100;
            var result = string.Format(Constants.Status, percent);
            return result;
        }

        private string TestAirConditioner(string manufacturer, string model)
        {
            var airConditioner = this.AirConditionerSystemDatabase.GetAirConditioner(manufacturer, model);
            if (airConditioner == null)
            {
                throw new NonExistantEntryException(Constants.NotExistingEntry);
            }

            var report = this.AirConditionerSystemDatabase.GetReport(manufacturer, model);
            if (report != null)
            {
                throw new DuplicateEntryException(Constants.DuplicateEntry);
            }

            var mark = airConditioner.Test();

            this.AirConditionerSystemDatabase.Reports.Add(
                new Report(airConditioner.Manufacturer, airConditioner.Model, mark));
            var result = string.Format(Constants.TestConditioner, model, manufacturer);
            return result;
        }

        private void ValidateParametersCount(Command command, int count)
        {
            if (command.Parameters.Length != count)
            {
                throw new InvalidOperationException(Constants.InvalidCommand);
            }
        }
    }
}