namespace VehicleParkSystem.Models
{
    public class Car : Vehicle
    {
        private const decimal CarRegularRate = 2;
        private const decimal CarOverTimeRate = 3.5m;

        public Car(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, CarRegularRate, CarOverTimeRate, reservedHours)
        {

        }
    }
}