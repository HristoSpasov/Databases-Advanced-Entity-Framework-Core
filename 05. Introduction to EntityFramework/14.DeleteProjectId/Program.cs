namespace _14.DeleteProjectId
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
                try
                {
                    EmployeeProject[] referenceTableRecordsToDelete = context.EmployeesProjects.Where(id => id.ProjectId == 2).ToArray();
                    context.EmployeesProjects.RemoveRange(referenceTableRecordsToDelete);

                    Project toDelete = context.Projects.Single(id => id.ProjectId == 2);
                    context.Projects.Remove(toDelete);

                    context.SaveChanges();
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }

                StringBuilder sb = new StringBuilder();

                context.Projects
                    .Select(n => new
                    {
                        n.Name
                    })
                    .Take(10)
                    .ToList()
                    .ForEach(r => sb.AppendLine($"{r.Name}"));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
