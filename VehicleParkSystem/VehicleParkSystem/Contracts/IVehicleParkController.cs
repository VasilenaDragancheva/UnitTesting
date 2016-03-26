namespace VehicleParkSystem.Contracts
{
    using System;

    public interface IVehicleParkController
    {
        string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid);

        string FindVehicle(string licensePlate);

        string FindVehiclesByOwner(string owner);

        string GetStatus();

        string InsertVehicle(IVehicle vehicle, int sectors, int placeNumber, DateTime startTime);
    }
}