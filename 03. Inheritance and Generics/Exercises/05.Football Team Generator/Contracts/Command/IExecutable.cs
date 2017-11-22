namespace _05.Football_Team_Generator.Contracts.Command
{
    using _05.Football_Team_Generator.Contracts.Core;

    public interface IExecutable
    {
        IFootballTeamCollection FootballTeamCollection { get; }

        string[] CommandTokens { get; }

        string Execute();
    }
}