namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using PhotoShare.Client.Contracts.Core;

    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(IServiceProvider provider, string cmdName)
        {
            string commandFullName = cmdName + "Command";

            // Get command type
            Type cmdType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)) && t.Name == commandFullName)
                .SingleOrDefault();

            if (cmdType == null)
            {
                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }

            // Get constructor services
            object[] services = cmdType.GetConstructors()
                                       .First()
                                       .GetParameters()
                                       .Select(pt => pt.ParameterType)
                                       .Select(provider.GetService)
                                       .ToArray();

            ICommand command = (ICommand)Activator.CreateInstance(cmdType, services);

            return command;
        }
    }
}