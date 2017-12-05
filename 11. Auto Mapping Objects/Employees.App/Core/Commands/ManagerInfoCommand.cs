namespace Employees.App.Core.Commands
{
    using AutoMapper;
    using Employees.App.Contracts.Core;
    using Employees.App.Models;
    using Employees.Models;
    using Employees.Services.Contracts;

    internal class ManagerInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ManagerInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int emplId = int.Parse(args[0]);

            Employee employee = this.employeeService.ById(emplId);

            ManagerDto manager = Mapper.Map<ManagerDto>(employee);

            return manager.ToString().Trim();
        }
    }
}