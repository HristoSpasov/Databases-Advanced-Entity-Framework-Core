namespace _10._DepssMore5Empl
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

                context.Departments
                    .Where(ec => ec.Employees.Count > 5)
                    .OrderBy(ec => ec.Employees.Count)
                    .ThenBy(dn => dn.Name)
                    .Select(e => new
                    {
                        DepartmentName = e.Name,
                        ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                        Employeess = e.Employees.ToList()
                    })
                    .ToList()
                    .ForEach(r => sb.AppendLine($"{r.DepartmentName} - {r.ManagerName}").AppendLine(string.Join(Environment.NewLine, r.Employeess.OrderBy(fn => fn.FirstName).ThenBy(ln => ln.LastName).Select(em => $"{em.FirstName + " " + em.LastName} - {em.JobTitle}"))).AppendLine(new string('-', 10)));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
