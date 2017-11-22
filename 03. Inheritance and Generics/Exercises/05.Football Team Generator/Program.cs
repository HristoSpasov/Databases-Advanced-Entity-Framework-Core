namespace _05.Football_Team_Generator
{
    using _05.Football_Team_Generator.Commands;
    using _05.Football_Team_Generator.Contracts.Command;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.IO;
    using _05.Football_Team_Generator.Core;
    using _05.Football_Team_Generator.IO;

    public class Program
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IOutputStoreManager outputStoreManager = new OutputStoreManager();
            ICommandSplit splitter = new CommandSpliter();
            IFootballTeamCollection footballTeamCollection = new FootballTeamCollection();

            IEngine engine = new Engine(reader, writer, outputStoreManager, splitter, footballTeamCollection);
            engine.Run();
        }
    }
}