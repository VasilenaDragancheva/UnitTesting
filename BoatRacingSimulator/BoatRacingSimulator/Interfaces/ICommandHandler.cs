namespace BoatRacingSimulator.Interfaces
{
    public interface ICommandHandler
    {
        string ExecuteCommand(string name, string[] parameters);
    }
}