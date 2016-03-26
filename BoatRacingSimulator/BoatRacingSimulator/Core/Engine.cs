namespace BoatRacingSimulator.Core
{
    using System;
    using System.Linq;

    using Interfaces;

    public class Engine : IEngine
    {
        public Engine(ICommandHandler commandHandler)
        {
            this.CommandHandler = commandHandler;
        }

        public Engine()
            : this(new CommandHandler())
        {
        }

        public ICommandHandler CommandHandler { get; private set; }

        public void Run()
        {
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var tokens = line.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var name = tokens[0];
                var parameters = tokens.Skip(1).ToArray();

                try
                {
                    string commandResult = this.CommandHandler.ExecuteCommand(name, parameters);
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