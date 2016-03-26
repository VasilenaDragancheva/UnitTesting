namespace AirConditionerTestingSystem.Core
{
    using System;

    using Contracts;

    using Models;

    using Utilities;

    public class Engine
    {
        private readonly Controller controller;

        private readonly IUserInterface userInterface;

        public Engine(IUserInterface userInterface, Controller controller)
        {
            this.controller = controller;
            this.userInterface = userInterface;
        }

        public Command Command { get; set; }

        public void Run()
        {
            while (true)
            {
                string line = this.userInterface.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }

                line = line.Trim();
                string result;
                try
                {
                    this.Command = this.ParseCommand(line);
                    result = this.controller.ExecuteCommand(this.Command);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                this.userInterface.WriteLine(result);
            }
        }

        private Command ParseCommand(string line)
        {
            try
            {
                var command = new Command();

                command.Name = line.Substring(0, line.IndexOf(' '));

                command.Parameters = line.Substring(line.IndexOf(' ') + 1)
                    .Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);

                return command;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Constants.InvalidCommand, ex);
            }
        }
    }
}