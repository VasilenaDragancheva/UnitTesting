namespace VehicleParkSystem.Core
{
    using System;

    using Contracts;

    public class Engine : IEngine
    {
        private readonly CommandDispatcher commandDispatcher;

        public Engine()
            : this(new CommandDispatcher())
        {
        }

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null)
                {
                    break;
                }

                commandLine.Trim();

                if (string.IsNullOrEmpty(commandLine))
                {
                    continue;
                }

                try
                {
                    var comando = new Command(commandLine);
                    string commandResult = this.commandDispatcher.ExecuteCommand(comando);
                    Console.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}