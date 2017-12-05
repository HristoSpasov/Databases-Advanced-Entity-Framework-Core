namespace Employees.Services
{
    using System;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Employees.Data;
    using Employees.Models;
    using Employees.Services.Contracts;

    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public Employee Add(Employee newEmployee)
        {
            this.context.Employees.Add(newEmployee);

            this.context.SaveChanges();

            return newEmployee;
        }

        public IQueryable<Tmodel> AllOlderThan<Tmodel>(int age)
        {
            // Calc target year
            DateTime now = DateTime.Now;
            now = now.AddYears(-age);

            return this.context.Employees.Where(e => e.Birthday < now).OrderByDescending(a => a.Salary).ProjectTo<Tmodel>();
        }

        public Employee ById(int id)
        {
            Employee toReturn = this.context.Employees.SingleOrDefault(i => i.Id == id);

            return toReturn;
        }

        public Employee Update(Employee updatedEmplyee)
        {
            Employee existingEmpl = this.ById(updatedEmplyee.Id);

            existingEmpl = updatedEmplyee;

            this.context.SaveChanges();

            return updatedEmplyee;
        }
    }
}