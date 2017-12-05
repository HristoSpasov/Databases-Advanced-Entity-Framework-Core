namespace PhotoShare.Client.Contracts.Core
{
    public interface ICommand
    {
        string Execute(params string[] cmdArgs);
    }
}