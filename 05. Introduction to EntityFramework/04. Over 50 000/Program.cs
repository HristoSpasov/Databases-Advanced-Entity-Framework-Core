namespace __04._Over_50_000
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
                string[] names = context.Employees.Select(e => new
                {
                    FirstName = e.FirstName,
                    Salary = e.Salary
                })
                .Where(s => s.Salary > 50000)
                .Select(n => n.FirstName)
                .OrderBy(n => n)
                .ToArray();

                StringBuilder output = new StringBuilder();

                foreach (var n in names)
                {
                    output.AppendLine(n);
                }

                FileCreator.CreateFile(output.ToString());
            }
        }
    }
}
