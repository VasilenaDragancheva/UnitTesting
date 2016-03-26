using System.Collections.Generic;

namespace VehicleParkSystem.Contracts
{
    public interface ICommand
    {
        string Name { get; }

        IDictionary<string, string> Parameters { get; }
    }
}