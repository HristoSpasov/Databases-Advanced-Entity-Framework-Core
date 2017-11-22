namespace _05.Football_Team_Generator.Exceptions
{
    using System;
    using _05.Football_Team_Generator.Utilities;

    public class MissingTeamException : Exception
    {
        public MissingTeamException(string teamName)
            : base(string.Format(Constants.MissingTeamExceptionMessage, teamName))
        {
        }
    }
}