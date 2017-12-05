namespace Employees.App.Core.Commands
{
    using System.Linq;
    using System.Text;
    using Employees.App.Contracts.Core;
    using Employees.App.Models;
    using Employees.Services.Contracts;

    internal class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ListEmployeesOlderThanCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int age = int.Parse(args[0]);

            UserManagerDto[] users = this.employeeService.AllOlderThan<UserManagerDto>(age).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (UserManagerDto user in users)
            {
                sb.AppendLine($"{user.FirstName} {user.LastName} - {user.Salary:F2} - Manager: {(!string.IsNullOrWhiteSpace(user.ManagerLastName) ? user.ManagerLastName : "[no manager]")}");
            }

            if (string.IsNullOrWhiteSpace(sb.ToString()))
            {
                return $"No employees older than {age}";
            }

            return sb.ToString().Trim();
        }
    }
}