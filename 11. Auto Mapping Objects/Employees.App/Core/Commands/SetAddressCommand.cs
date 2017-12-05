namespace Employees.App.Core.Commands
{
    using System.Linq;
    using Employees.App.Contracts.Core;
    using Employees.Models;
    using Employees.Services.Contracts;

    internal class SetAddressCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetAddressCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int emplId = int.Parse(args.First());
            string address = string.Join(" ", args.Skip(1).Select(s => s).ToArray());

            Employee emplToUpdate = this.employeeService.ById(emplId);
            emplToUpdate.Address = address;

            this.employeeService.Update(emplToUpdate);

            return $"{emplToUpdate.FirstName} {emplToUpdate.LastName} address is set to {emplToUpdate.Address}";
        }
    }
}