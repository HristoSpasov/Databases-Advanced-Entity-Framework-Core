namespace _09._Employee_147
{
    using System;
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
                    .Where(em => em.EmployeeId == 147)
                    .Select(e => new
                    {
                        Name = e.FirstName + " " + e.LastName,
                        JobTitle = e.JobTitle,
                        Projects = e.EmployeesProjects.Select(n => n.Project.Name).OrderBy(n => n).ToList()
                    })
                    .ToList()
                    .ForEach(r => sb.AppendLine($"{r.Name} - {r.JobTitle}").AppendLine(string.Join(Environment.NewLine, r.Projects)));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
