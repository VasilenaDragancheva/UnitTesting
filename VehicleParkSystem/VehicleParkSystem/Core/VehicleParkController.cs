namespace VehicleParkSystem.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;

    public class VehicleParkController : IVehicleParkController
    {
        public VehicleParkController(int numberOfSectors, int placesPerSector)
        {
            this.Layout = new Layout(numberOfSectors, placesPerSector);
            this.Data = new VehicleParkData(numberOfSectors);
        }

        public VehicleParkController(Layout layout, IVehicleParkData data)
        {
            this.Layout = layout;
            this.Data = data;
        }

        public Layout Layout { get; private set; }

        public IVehicleParkData Data { get; private set; }

        public string ExitVehicle(string licensePlate, DateTime endTime, decimal money)
        {
            var vehicle = this.Data.VehiclesByLincensePlate.ContainsKey(licensePlate)
                              ? this.Data.VehiclesByLincensePlate[licensePlate]
                              : null;
            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", licensePlate);
            }

            var ticket = this.GetTicket(money, vehicle, endTime);

            this.Data.RemoveVehicle(vehicle);

            return ticket;
        }

        public string FindVehicle(string licensePlate)
        {
            var vehicle = this.Data.VehiclesByLincensePlate.ContainsKey(licensePlate)
                              ? this.Data.VehiclesByLincensePlate[licensePlate]
                              : null;
            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", licensePlate);
            }

            return this.Input(new[] { vehicle });
        }

        public string FindVehiclesByOwner(string owner)
        {
            if (!this.Data.VehiclesByOwner.ContainsKey(owner))
            {
                return string.Format("No vehicles by {0}", owner);
            }

            return string.Join(Environment.NewLine, this.Input(this.Data.VehiclesByOwner[owner]));
        }

        public string GetStatus()
        {
            var places =
                this.Data.OccupiedPlacesBySector.Select(
                    (s, i) =>
                    string.Format(
                        "Sector {0}: {1} / {2} ({3}% full)",
                        i + 1,
                        s,
                        this.Layout.PlacesPerSector,
                       Math.Ceiling((decimal)s / this.Layout.PlacesPerSector * 100)));

            return string.Join(Environment.NewLine, places);
        }

        public string InsertVehicle(IVehicle vehicle, int sector, int place, DateTime startTime)
        {
            if (sector > this.Layout.Sectors)
            {
                return string.Format("There is no sector {0} in the park", sector);
            }

            if (place > this.Layout.PlacesPerSector)
            {
                return string.Format("There is no place {0} in sector {1}", place, sector);
            }

            if (this.Data.VehiclesByPlace.ContainsKey(string.Format("({0},{1})", sector, place)))
            {
                return string.Format("The place ({0},{1}) is occupied", sector, place);
            }

            if (this.Data.VehiclesByLincensePlate.ContainsKey(vehicle.LicensePlate))
            {
                return string.Format(
                    "There is already a vehicle with license plate {0} in the park",
                    vehicle.LicensePlate);
            }

            this.Data.AddVehicle(vehicle, sector, place, startTime);

            return string.Format("{0} parked successfully at place ({1},{2})", vehicle.GetType().Name, sector, place);
        }

        private string GetTicket(decimal money, IVehicle vehicle, DateTime endTime)
        {
            var startTime = this.Data.TimeByVehicle[vehicle];
            int duration = (int)Math.Round((endTime - startTime).TotalHours);
            var ticket = new StringBuilder();
            ticket.AppendLine(new string('*', 20))
                .AppendFormat("{0}", vehicle)
                .AppendLine()
                .AppendFormat("at place {0}", this.Data.PlacesByVehicle[vehicle])
                .AppendLine()
                .AppendFormat("Rate: ${0:F2}", vehicle.ReservedHours * vehicle.RegularRate)
                .AppendLine()
                .AppendFormat(
                    "Overtime rate: ${0:F2}",
                    duration > vehicle.ReservedHours ? (duration - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)
                .AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat(
                    "Total: ${0:F2}",
                    vehicle.ReservedHours * vehicle.RegularRate
                    + (duration > vehicle.ReservedHours ? (duration - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))
                .AppendLine()
                .AppendFormat("Paid: ${0:F2}", money)
                .AppendLine()
                .AppendFormat(
                    "Change: ${0:F2}",
                    money
                    - ((vehicle.ReservedHours * vehicle.RegularRate)
                       + (duration > vehicle.ReservedHours
                              ? (duration - vehicle.ReservedHours) * vehicle.OvertimeRate
                              : 0)))
                .AppendLine()
                .Append(new string('*', 20));
            return ticket.ToString();
        }

        private string Input(IEnumerable<IVehicle> carros)
        {
            return string.Join(
                Environment.NewLine,
                carros.Select(
                    vehicle =>
                    string.Format(
                        "{0}{1}Parked at {2}",
                        vehicle.ToString(),
                        Environment.NewLine,
                        this.Data.PlacesByVehicle[vehicle])));
        }
    }
}