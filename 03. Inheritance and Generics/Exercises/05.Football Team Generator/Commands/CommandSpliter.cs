namespace _05.Football_Team_Generator.Commands
{
    using System;
    using _05.Football_Team_Generator.Contracts.Command;
    using _05.Football_Team_Generator.Utilities;

    public class CommandSpliter : ICommandSplit
    {
        private string splitString;

        public CommandSpliter()
        {
            this.splitString = Constants.DefaultLineSplitter;
        }

        public CommandSpliter(string splitString)
        {
            this.splitString = splitString;
        }

        public string[] Split(string toSplit)
        {
            return toSplit.Split(this.splitString.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}