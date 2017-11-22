namespace _05.Football_Team_Generator.Core
{
    using _05.Football_Team_Generator.Contracts.Command;
    using _05.Football_Team_Generator.Contracts.Core;
    using _05.Football_Team_Generator.Contracts.IO;
    using _05.Football_Team_Generator.Factories;
    using _05.Football_Team_Generator.Utilities;

    public class Engine : IEngine
    {
        public Engine(IReader reader, IWriter writer, IOutputStoreManager outputStoreManager, ICommandSplit lineSplit, IFootballTeamCollection footballTeamCollection)
        {
            this.Reader = reader;
            this.Writer = writer;
            this.OutputStoremanager = outputStoreManager;
            this.LineSplitter = lineSplit;
            this.FootballTeamCollection = footballTeamCollection;
        }

        public IReader Reader { get; private set; }

        public IWriter Writer { get; private set; }

        public IOutputStoreManager OutputStoremanager { get; private set; }

        public ICommandSplit LineSplitter { get; private set; }

        public IFootballTeamCollection FootballTeamCollection { get; private set; }

        public IExecutable CommandFacttory { get; private set; }

        public void Run()
        {
            while (true)
            {
                string line = this.Reader.Read();

                if (line == Constants.TerminateCommand)
                {
                    break;
                }

                string[] lineTokens = this.LineSplitter.Split(line);

                IExecutable command = CommandFactory.CreateCommmand(lineTokens, this.FootballTeamCollection);
                string commandResult = command.Execute();

                this.OutputStoremanager.AppendLine(commandResult);
            }

            // Print
            this.Writer.Write(this.OutputStoremanager.GetOutput());
        }
    }
}