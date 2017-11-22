namespace _05.Football_Team_Generator.Utilities
{
    using _05.Football_Team_Generator.Exceptions;

    public static class Validator
    {
        public static void ValidateStats(string type, int value)
        {
            if (value < Constants.StatMinValue || value > Constants.StatMaxValue)
            {
                throw new InvalidStatsValueException(type);
            }
        }
    }
}