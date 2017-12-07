namespace TeamBuilder.App.Contracts
{
    internal interface ICommandFactory
    {
        ICommand GetCommand(string commandName);
    }
}