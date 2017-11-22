namespace _05.Football_Team_Generator.Commands
{
    using _05.Football_Team_Generator.Contracts.Command;
    using _05.Football_Team_Generator.Contracts.Core;

    public abstract class Command : IExecutable
    {
        public Command(IFootballTeamCollection footballTeamCollection, string[] commandTokens)
        {
            this.FootballTeamCollection = footballTeamCollection;
            this.CommandTokens = commandTokens;
        }

        public IFootballTeamCollection FootballTeamCollection { get; private set; }

        public string[] CommandTokens { get; private set; }

        public abstract string Execute();
    }
}