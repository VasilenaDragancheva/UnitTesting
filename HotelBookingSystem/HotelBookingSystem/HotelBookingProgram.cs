namespace HotelBookingSystem
{
    using System.Globalization;
    using System.Threading;

    public class HotelBookingProgram
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var engine = new Engine();
            engine.Run();
        }
    }
}