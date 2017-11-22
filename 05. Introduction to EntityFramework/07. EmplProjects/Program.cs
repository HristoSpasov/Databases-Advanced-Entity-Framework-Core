namespace _07._EmplProjects
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using P02_DatabaseFirst;
    using P02_DatabaseFirst.Data;

    public class Program
    {
        public static void Main()
        {
            using (SoftUniContext db = new SoftUniContext())
            {
                StringBuilder sb = new StringBuilder();

                db.Employees
                  .Where(ep => ep.EmployeesProjects.Any(epr => epr.Project.StartDate.Year >= 2001 && epr.Project.StartDate.Year <= 2003))
                  .Take(30)
                  .Select(ep => new
                  {
                      EmplFirstName = ep.FirstName,
                      EmplLastName = ep.LastName,
                      ManagerFirstName = ep.Manager.FirstName,
                      ManagerLastName = ep.Manager.LastName,
                      Projects = (ep.EmployeesProjects.Any(p => p.Project.StartDate >= new DateTime(2001, 1, 1) && p.Project.StartDate <= new DateTime(2003, 1, 1)) ?
                                 ep.EmployeesProjects.Select(p => new
                                 {
                                     ProjectName = p.Project.Name,
                                     ProjectStartDate = p.Project.StartDate.ToString(@"M/d/yyyy h:mm:ss tt"),
                                     ProjectEndDate = p.Project.EndDate.HasValue ? p.Project.EndDate.Value.ToString(@"M/d/yyyy h:mm:ss tt") : "not finished",
                                 }).ToArray()
                                 :
                                 null)
                  })
                  .Take(30)
                  .Where(e => e.Projects != null)
                  .ToList()
                  .ForEach(e => sb.AppendLine($"{e.EmplFirstName} {e.EmplLastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}").AppendLine(string.Join(Environment.NewLine, e.Projects.Select(p => $"--{p.ProjectName} - {p.ProjectStartDate} - {p.ProjectEndDate}"))));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
