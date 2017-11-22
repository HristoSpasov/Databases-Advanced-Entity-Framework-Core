namespace _12.IncreaseSalarie
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

                var emplToUpdate = context.Employees
                    .Where(dn => dn.Department.Name == "Engineering" || dn.Department.Name == "Tool Design" || dn.Department.Name == "Marketing" || dn.Department.Name == "Information Services")
                    .OrderBy(fn => fn.FirstName)
                    .ThenBy(ln => ln.LastName)
                    .ToList();

                foreach (var e in emplToUpdate)
                {
                    e.Salary *= 1.12m;
                    sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
                }

                
                context.SaveChanges();

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
