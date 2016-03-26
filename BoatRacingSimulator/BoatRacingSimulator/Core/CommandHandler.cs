namespace BoatRacingSimulator.Core
{
    using System;

    using Controllers;

    using Interfaces;

    public class CommandHandler : ICommandHandler
    {
        public CommandHandler(IBoatSimulatorController controller)
        {
            this.Controller = controller;
        }

        public CommandHandler()
            : this(new BoatSimulatorController())
        {
        }

        public IBoatSimulatorController Controller { get; private set; }

        public string ExecuteCommand(string name, string[] parameters)
        {
            switch (name)
            {
                case "CreateBoatEngine":

                    return this.Controller.CreateBoatEngine(
                        parameters[0], 
                        int.Parse(parameters[1]), 
                        int.Parse(parameters[2]), 
                        parameters[3]);

                case "CreateRowBoat":
                    return this.Controller.CreateRowBoat(
                        parameters[0], 
                        int.Parse(parameters[1]), 
                        int.Parse(parameters[2]));
                case "CreateSailBoat":
                    return this.Controller.CreateSailBoat(
                        parameters[0], 
                        int.Parse(parameters[1]), 
                        int.Parse(parameters[2]));
                case "CreatePowerBoat":
                    return this.Controller.CreatePowerBoat(
                        parameters[0], 
                        int.Parse(parameters[1]), 
                        parameters[2], 
                        parameters[3]);
                case "CreateYacht":
                    return this.Controller.CreateYacht(
                        parameters[0], 
                        int.Parse(parameters[1]), 
                        parameters[2], 
                        int.Parse(parameters[3]));
                case "OpenRace":
                    return this.Controller.OpenRace(
                        int.Parse(parameters[0]), 
                        int.Parse(parameters[1]), 
                        int.Parse(parameters[2]), 
                        bool.Parse(parameters[3]));
                case "SignUpBoat":
                    return this.Controller.SignUpBoat(parameters[0]);
                case "StartRace":
                    return this.Controller.StartRace();
                case "GetStatistic":
                    return this.Controller.GetStatistic();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}