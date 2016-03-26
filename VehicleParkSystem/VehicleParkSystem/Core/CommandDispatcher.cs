namespace VehicleParkSystem.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Contracts;

    using Models;

    public class CommandDispatcher
    {
        public IVehicleParkController VehiclePark { get; set; }

        public string ExecuteCommand(ICommand command)
        {
            if (command.Name != "SetupPark" && this.VehiclePark == null)
            {
                return "The vehicle park has not been set up";
            }

            string commandResult = string.Empty;
            switch (command.Name)
            {
                case "SetupPark":
                    int sectors = int.Parse(command.Parameters["sectors"]);
                    int places = int.Parse(command.Parameters["placesPerSector"]);
                    this.VehiclePark = new VehicleParkController(sectors, places);
                    commandResult = "Vehicle park created";
                    break;
                case "Park":
                    IVehicle vehicle = this.VehicleCreate(command.Parameters);
                    int sector = int.Parse(command.Parameters["sector"]);
                    int place = int.Parse(command.Parameters["place"]);
                    var startTime = DateTime.Parse(command.Parameters["time"]);
                    commandResult = this.VehiclePark.InsertVehicle(vehicle, sector, place, startTime);

                    break;

                case "Exit":
                    commandResult = this.VehiclePark.ExitVehicle(
                        command.Parameters["licensePlate"], 
                        DateTime.Parse(command.Parameters["time"], null, DateTimeStyles.RoundtripKind), 
                        decimal.Parse(command.Parameters["paid"]));
                    break;
                case "Status":
                    commandResult = this.VehiclePark.GetStatus();
                    break;
                case "FindVehicle":
                    commandResult = this.VehiclePark.FindVehicle(command.Parameters["licensePlate"]);
                    break;
                case "VehiclesByOwner":
                    commandResult = this.VehiclePark.FindVehiclesByOwner(command.Parameters["owner"]);
                    break;
                default:
                    throw new InvalidOperationException("Invalid command.");
            }

            return commandResult;
        }

        private IVehicle VehicleCreate(IDictionary<string, string> dictionary)
        {
            var type = dictionary["type"];
            var licensePlate = dictionary["licensePlate"];
            var owner = dictionary["owner"];
            var hours = int.Parse(dictionary["hours"]);

            switch (type)
            {
                case "car":
                    return new Car(licensePlate, owner, hours);
                case "truck":
                    return new Truck(licensePlate, owner, hours);
                case "motorbike":
                    return new Motorbike(licensePlate, owner, hours);
                default:
                    return null;
            }
        }
    }
}