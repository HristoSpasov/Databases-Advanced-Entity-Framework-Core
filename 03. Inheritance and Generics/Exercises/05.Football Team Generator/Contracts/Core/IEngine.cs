namespace _05.Football_Team_Generator.Contracts.Core
{
    using _05.Football_Team_Generator.Contracts.Command;
    using _05.Football_Team_Generator.Contracts.IO;

    public interface IEngine
    {
        IReader Reader { get; }

        IWriter Writer { get; }

        IOutputStoreManager OutputStoremanager { get; }

        ICommandSplit LineSplitter { get; }

        IFootballTeamCollection FootballTeamCollection { get; }

        void Run();
    }
}