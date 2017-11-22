namespace _05.Football_Team_Generator.Utilities
{
    public static class Constants
    {
        // Stats constants
        public const int StatMinValue = 0;

        public const int StatMaxValue = 100;

        // Exceptions messages
        public const string InvalidNameExceptionMessage = "A name should not be empty.";

        public const string InvalidStatsValueExceptionMessage = "{0} should be between 0 and 100.";
        public const string MissingPlayerExceptionMessage = "Player {0} is not in {1} team.";
        public const string MissingTeamExceptionMessage = "Team {0} does not exist.";

        // Default line splitter
        public const string DefaultLineSplitter = ";";

        // Terminating line
        public const string TerminateCommand = "END";

        // Stats
        public static readonly string[] Stats = { "Endurance", "Sprint", "Dribble", "Passing", "Shooting" };
    }
}