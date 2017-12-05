namespace Employees.App.Core.Commands
{
    using Employees.App.Contracts.Core;
    using Employees.Models;
    using Employees.Services.Contracts;

    internal class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public AddEmployeeCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            Employee newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary,
            };

            this.employeeService.Add(newEmployee);

            return "New employee successfully added to database.";
        }
    }
}