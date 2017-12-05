namespace Employees.App.Contracts.Core
{
    using System;

    internal interface ICommandFactory
    {
        ICommand GetCommand(string cmd, IServiceProvider provider);
    }
}