namespace VehicleParkSystem.Contracts
{
    using System;
    using System.Collections.Generic;

    using Wintellect.PowerCollections;

    public interface IVehicleParkData
    {
        IDictionary<IVehicle, string> PlacesByVehicle { get; set; }

        IDictionary<string, IVehicle> VehiclesByPlace { get; set; }

        IDictionary<string, IVehicle> VehiclesByLincensePlate { get; set; }

        IDictionary<IVehicle, DateTime> TimeByVehicle { get; set; }

        MultiDictionary<string, IVehicle> VehiclesByOwner { get; set; }

        int[] OccupiedPlacesBySector { get; set; }

        void AddVehicle(IVehicle vehicle, int sector, int place, DateTime startTime);

        void RemoveVehicle(IVehicle vehicle);
    }
}