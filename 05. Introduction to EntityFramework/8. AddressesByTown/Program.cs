namespace _8._AddressesByTown
{
    using System.Linq;
    using System.Text;
    using P02_DatabaseFirst;
    using P02_DatabaseFirst.Data;

    public class Program
    {
        public static void Main()
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                StringBuilder sb = new StringBuilder();

                context.Addresses
                    .Select(a => new
                    {
                        TownName = a.Town.Name,
                        AddressText = a.AddressText,
                        EmployeesCount = a.Employees.Count
                    })
                    .OrderByDescending(ec => ec.EmployeesCount)
                    .ThenBy(tn => tn.TownName)
                    .ThenBy(at => at.AddressText)
                    .Take(10)
                    .ToList()
                    .ForEach(r => sb.AppendLine($"{r.AddressText}, {r.TownName} - {r.EmployeesCount} employees"));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
