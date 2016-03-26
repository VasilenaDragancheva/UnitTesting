namespace VehicleParkSystem.Models
{
    public class Truck : Vehicle
    {
        private const decimal TruckRegularRate = 4.75m;

        private const decimal TruckOverTimeRate = 6.2m;

        public Truck(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, TruckRegularRate, TruckOverTimeRate, reservedHours)
        {

        }
    }
}