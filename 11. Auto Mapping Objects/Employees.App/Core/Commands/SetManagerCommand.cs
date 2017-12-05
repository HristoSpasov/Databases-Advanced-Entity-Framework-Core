namespace Employees.App.Core.Commands
{
    using Employees.App.Contracts.Core;
    using Employees.Models;
    using Employees.Services.Contracts;

    internal class SetManagerCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetManagerCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            Employee toUpdate = this.employeeService.ById(employeeId);
            toUpdate.ManagerId = managerId;

            this.employeeService.Update(toUpdate);

            return $"{toUpdate.Manager.FirstName} {toUpdate.Manager.LastName} set as manager of {toUpdate.FirstName} {toUpdate.LastName}";
        }
    }
}