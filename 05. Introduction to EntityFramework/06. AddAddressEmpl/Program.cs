namespace _06._AddAddressEmpl
{
    using System.Linq;
    using System.Text;
    using P02_DatabaseFirst;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class Program
    {
        public static void Main()
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                Address newAddress = new Address()
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                Employee nakovEmployee = context.Employees.SingleOrDefault(ln => ln.LastName == "Nakov");
                nakovEmployee.Address = newAddress;

                context.SaveChanges();

                StringBuilder output = new StringBuilder();

                context.Employees
                       .Select(e => new
                       {
                           EmployeeAddressId = e.AddressId,
                           AddtessText = e.Address.AddressText
                       })
                       .OrderByDescending(aId => aId.EmployeeAddressId)
                       .Take(10)
                       .ToList()
                       .ForEach(at => output.AppendLine(at.AddtessText));

                FileCreator.CreateFile(output.ToString());
            }
        }
    }
}
