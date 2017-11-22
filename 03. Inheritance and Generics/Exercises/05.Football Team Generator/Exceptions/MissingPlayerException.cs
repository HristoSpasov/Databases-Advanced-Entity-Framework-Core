namespace _05.Football_Team_Generator.Exceptions
{
    using System;
    using _05.Football_Team_Generator.Utilities;

    public class MissingPlayerException : Exception
    {
        public MissingPlayerException(string playerName, string teamName)
            : base(string.Format(Constants.MissingPlayerExceptionMessage, playerName, teamName))
        {
        }
    }
}