namespace TeamBuilder.App.Contracts
{
    public interface ICommand
    {
        string Execute(params string[] args);
    }
}