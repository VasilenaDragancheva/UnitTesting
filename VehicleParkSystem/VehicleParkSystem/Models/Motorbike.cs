namespace VehicleParkSystem.Models
{
    public class Motorbike : Vehicle
    {
        private const decimal MotorbikeRegularRate = 1.35m;
        private const decimal MotorbikeOverTimeRate = 3;

        public Motorbike(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, MotorbikeRegularRate, MotorbikeOverTimeRate, reservedHours)
        {

        }
    }
}
