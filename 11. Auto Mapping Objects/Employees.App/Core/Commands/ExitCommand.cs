namespace Employees.App.Core.Commands
{
    using Employees.App.Contracts.Core;

    internal class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            return "See ya later alligator";
        }
    }
}