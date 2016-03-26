namespace BoatRacingSimulator.Utility
{
    using System;

    public static class Validator
    {
        public static void ValidateModelLength(string value, int minModelLength)
        {
            if (value.Length < minModelLength)
            {
                throw new ArgumentException(string.Format(Constants.IncorrectModelLenghtMessage, minModelLength));
            }
        }

        public static void ValidatePropertyValue(int value, string propertyName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(string.Format(Constants.IncorrectPropertyValueMessage, propertyName));
            }
        }
    }
}