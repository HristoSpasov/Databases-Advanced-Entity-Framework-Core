namespace PhotoShare.Client.Contracts.Core
{
    using System;

    public interface ICommandFactory
    {
        ICommand GetCommand(IServiceProvider provider, string cmdName);
    }
}