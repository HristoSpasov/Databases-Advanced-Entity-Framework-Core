namespace Employees.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Employees.App.Contracts.Core;
    using Microsoft.Extensions.DependencyInjection;

    internal class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(string cmd, IServiceProvider provider)
        {
            string commandFullName = cmd + "Command";

            Type cmdType = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(t => t.Name == commandFullName)
                                   .SingleOrDefault();

            if (cmdType == null)
            {
                throw new ArgumentException("Invalid command!");
            }

            object[] ctorParamTypes = cmdType.GetConstructors()
                                             .Single()
                                             .GetParameters()
                                             .Select(pt => pt.ParameterType)
                                             .Select(provider.GetService)
                                             .ToArray();

            ICommand cmdInstance = (ICommand)Activator.CreateInstance(cmdType, ctorParamTypes);

            return cmdInstance;
        }
    }
}