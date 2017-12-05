namespace Employees.App.Core.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Employees.App.Contracts.Core;
    using Employees.Models;
    using Employees.Services.Contracts;

    internal class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetBirthdayCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int emplId = int.Parse(args.First());
            DateTime birthday = DateTime.ParseExact(args.Last(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            Employee emplToUpdate = this.employeeService.ById(emplId);
            emplToUpdate.Birthday = birthday;

            this.employeeService.Update(emplToUpdate);

            return $"{emplToUpdate.FirstName} {emplToUpdate.LastName} birthday is set to {emplToUpdate.Birthday.Value.ToString("dd-MM-yyyy")}";
        }
    }
}