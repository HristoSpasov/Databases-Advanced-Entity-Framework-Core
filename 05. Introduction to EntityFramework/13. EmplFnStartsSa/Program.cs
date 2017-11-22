namespace _13._EmplFnStartsSa
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

                context.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .Select(em => new
                    {
                        FirstName = em.FirstName,
                        LastName = em.LastName,
                        JobTitle = em.JobTitle,
                        Salary = em.Salary
                    })
                    .OrderBy(fn => fn.FirstName)
                    .ThenBy(ln => ln.LastName)
                    .ToList()
                    .ForEach(r => sb.AppendLine($"{r.FirstName} {r.LastName} - {r.JobTitle} - (${r.Salary:F2})"));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
