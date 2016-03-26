namespace AirConditionerTestingSystem.Contracts
{
    public interface IAirConditioner
    {
        string Manufacturer { get; }

        string Model { get; }

        int Test();
    }
}