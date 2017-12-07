namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using TeamBuilder.App.Contracts;

    public class CommandFactory : ICommandFactory
    {
        private const string InvallidCommandMessage = "Command {0} not valid!";
        private readonly IServiceProvider serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand GetCommand(string commandName)
        {
            string fullCommandName = commandName + "Command";

            Type commandType = Assembly.GetExecutingAssembly()
                                                           .GetTypes()
                                                           .Where(t => t.GetInterfaces().Contains(typeof(ICommand)) && t.Name == fullCommandName)
                                                           .SingleOrDefault();

            if (commandType == null)
            {
                throw new NotSupportedException(string.Format(InvallidCommandMessage, commandName));
            }

            object[] commandDependenciesInstances = commandType
                                                           .GetConstructors()
                                                           .SingleOrDefault()
                                                           .GetParameters()
                                                           .Select(t => t.ParameterType)
                                                           .Select(t => this.serviceProvider.GetService(t))
                                                           .ToArray();

            ICommand commandInstance = (ICommand)Activator.CreateInstance(commandType, commandDependenciesInstances);

            return commandInstance;
        }
    }
}