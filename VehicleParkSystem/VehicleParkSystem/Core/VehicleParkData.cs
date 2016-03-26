namespace VehicleParkSystem
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    using Wintellect.PowerCollections;

    public class VehicleParkData : IVehicleParkData
    {
        public VehicleParkData(int numberOfSectors)
        {
            this.PlacesByVehicle = new Dictionary<IVehicle, string>();
            this.VehiclesByPlace = new Dictionary<string, IVehicle>();
            this.VehiclesByLincensePlate = new Dictionary<string, IVehicle>();
            this.TimeByVehicle = new Dictionary<IVehicle, DateTime>();
            this.VehiclesByOwner = new MultiDictionary<string, IVehicle>(false);
            this.OccupiedPlacesBySector = new int[numberOfSectors];
        }

        public IDictionary<IVehicle, string> PlacesByVehicle { get; set; }

        public IDictionary<string, IVehicle> VehiclesByPlace { get; set; }

        public IDictionary<string, IVehicle> VehiclesByLincensePlate { get; set; }

        public IDictionary<IVehicle, DateTime> TimeByVehicle { get; set; }

        public MultiDictionary<string, IVehicle> VehiclesByOwner { get; set; }

        public int[] OccupiedPlacesBySector { get; set; }

        public void RemoveVehicle(IVehicle vehicle)
        {
            int sector =
               int.Parse(
                   this.PlacesByVehicle[vehicle].Split(
                       new[] { "(", ",", ")" },
                       StringSplitOptions.RemoveEmptyEntries)[0]);
            this.VehiclesByPlace.Remove(this.PlacesByVehicle[vehicle]);
            this.PlacesByVehicle.Remove(vehicle);
            this.VehiclesByLincensePlate.Remove(vehicle.LicensePlate);
            this.TimeByVehicle.Remove(vehicle);
            this.VehiclesByOwner.Remove(vehicle.Owner, vehicle);
            this.OccupiedPlacesBySector[sector - 1]--;
        }

        public void AddVehicle(IVehicle vehicle, int sector, int place, DateTime startTime)
        {
            this.PlacesByVehicle[vehicle] = string.Format("({0},{1})", sector, place);
            this.VehiclesByPlace[string.Format("({0},{1})", sector, place)] = vehicle;
            this.VehiclesByLincensePlate[vehicle.LicensePlate] = vehicle;
            this.TimeByVehicle[vehicle] = startTime;
            this.VehiclesByOwner[vehicle.Owner].Add(vehicle);
            this.OccupiedPlacesBySector[sector - 1]++;
        }
    }
}