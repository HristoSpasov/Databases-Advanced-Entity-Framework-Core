namespace Employees.App.Core.Commands
{
    using System.Linq;
    using AutoMapper;
    using Employees.App.Contracts.Core;
    using Employees.App.Models;
    using Employees.Models;
    using Employees.Services.Contracts;

    internal class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public EmployeeInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int emplId = int.Parse(args.First());

            Employee employee = this.employeeService.ById(emplId);

            EmployeeDto employeeInfo = Mapper.Map<EmployeeDto>(employee);

            return employeeInfo.ToString().Trim();
        }
    }
}