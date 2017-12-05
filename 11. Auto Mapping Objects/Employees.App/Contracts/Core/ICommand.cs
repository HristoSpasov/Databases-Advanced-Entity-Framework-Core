namespace Employees.App.Contracts.Core
{
    internal interface ICommand
    {
        string Execute(params string[] args);
    }
}