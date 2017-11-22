namespace _05.Football_Team_Generator.Exceptions
{
    using System;
    using _05.Football_Team_Generator.Utilities;

    public class InvalidNameException : Exception
    {
        public InvalidNameException()
            : base(Constants.InvalidNameExceptionMessage)
        {
        }
    }
}