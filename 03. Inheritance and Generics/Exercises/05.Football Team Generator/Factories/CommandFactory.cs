namespace _05.Football_Team_Generator.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using _05.Football_Team_Generator.Contracts.Command;
    using _05.Football_Team_Generator.Contracts.Core;

    public static class CommandFactory
    {
        public static IExecutable CreateCommmand(string[] lineTokens, IFootballTeamCollection footballTeamCollection)
        {
            string commandName = lineTokens[0];
            string[] toProcess = lineTokens.Skip(1).ToArray();

            object[] parametersForConstructin = new object[]
            {
                footballTeamCollection, toProcess
            };

            Type commandType = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .First(t => t.Name == commandName);

            IExecutable cmd = (IExecutable)Activator.CreateInstance(commandType, parametersForConstructin);

            return cmd;
        }
    }
}