namespace AirConditionerTestingSystem
{
    using Core;

    using UI;

    public class Program
    {
        public static void Main()
        {
            var userInterface = new ConsoleUserInterface();
            var controller = new Controller();
            var engine = new Engine(userInterface, controller);
            engine.Run();
        }
    }
}