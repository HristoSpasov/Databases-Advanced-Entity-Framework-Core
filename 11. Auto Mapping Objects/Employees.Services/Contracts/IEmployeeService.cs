namespace Employees.Services.Contracts
{
    using System.Linq;
    using Employees.Models;

    public interface IEmployeeService
    {
        Employee ById(int id);

        Employee Add(Employee newEmployee);

        Employee Update(Employee updatedEmplyee);

        IQueryable<Tmodel> AllOlderThan<Tmodel>(int age);
    }
}