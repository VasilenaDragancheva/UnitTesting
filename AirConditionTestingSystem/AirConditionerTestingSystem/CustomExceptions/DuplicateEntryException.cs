namespace AirConditionerTestingSystem.CustomExceptions
{
    using System;

    public class DuplicateEntryException : DivideByZeroException
    {
        public DuplicateEntryException(string message)
            : base(message)
        {
        }
    }
}