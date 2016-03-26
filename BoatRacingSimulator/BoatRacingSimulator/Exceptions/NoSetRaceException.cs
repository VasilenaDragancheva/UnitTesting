namespace BoatRacingSimulator.Exceptions
{
    using System;

    public class NoSetRaceException : Exception
    {
        public NoSetRaceException(string message)
            : base(message)
        {
        }
    }
}