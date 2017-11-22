namespace _05._Employees_RnD
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
                StringBuilder output = new StringBuilder();

                context.Employees
                    .Select(ed => new
                    {
                        FirstName = ed.FirstName,
                        LastName = ed.LastName,
                        Department = ed.Department,
                        Salary = ed.Salary
                    })
                    .Where(d => d.Department.Name == "Research and Development")
                    .OrderBy(s => s.Salary)
                    .ThenByDescending(fn => fn.FirstName)
                    .ToList()
                    .ForEach(e => output.AppendLine($"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:F2}"));

                FileCreator.CreateFile(output.ToString());
            }
        }
    }
}
