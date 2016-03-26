using System;

namespace VehicleParkSystem.Contracts
{
    using Models;

    public interface IVehiclePark
    {
        string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid);

        string FindVehicle(string licensePlate);

        string FindVehiclesByOwner(string owner);

        string GetStatus();

        string InsertCar(Car car, int sector, int placeNumber, DateTime startTime);

        string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime);

        string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime);
    }
}