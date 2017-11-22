namespace _05.Football_Team_Generator.Exceptions
{
    using System;
    using _05.Football_Team_Generator.Utilities;

    public class InvalidStatsValueException : Exception
    {
        public InvalidStatsValueException(string statsType)
            : base(string.Format(Constants.InvalidStatsValueExceptionMessage, statsType))
        {
        }
    }
}