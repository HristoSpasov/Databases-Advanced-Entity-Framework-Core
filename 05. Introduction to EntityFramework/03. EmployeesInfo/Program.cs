namespace __03._EmployeesInfo
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
                var employees = context.Employees
                                       .Select(e => new
                                       {
                                           e.EmployeeId,
                                           e.FirstName,
                                           e.LastName,
                                           e.MiddleName,
                                           e.JobTitle,
                                           e.Salary
                                       }
                                      ).ToArray();

                StringBuilder output = new StringBuilder();

                foreach (var e in employees.OrderBy(e => e.EmployeeId))
                {
                    output.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");
                }

                FileCreator.CreateFile(output.ToString());
            }
        }
    }
}