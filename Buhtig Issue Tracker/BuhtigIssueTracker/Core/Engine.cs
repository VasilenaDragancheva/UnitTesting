namespace BuhtigIssueTracker.Core
{
    using System;

    using Contracts;

    public class Engine : IEngine
    {
        private readonly CommandDispatcher commandDispatcher;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public Engine()
            : this(new CommandDispatcher())
        {
        }

        public void Run()
        {
            while (true)
            {
                string url = Console.ReadLine();
                if (url == null)
                {
                    break;
                }

                url = url.Trim();
                if (string.IsNullOrEmpty(url))
                {
                    continue;
                }

                try
                {
                    var endPoint = new Endpoint(url);
                    string viewResult = this.commandDispatcher.DispatchAction(endPoint);
                    Console.WriteLine(viewResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}