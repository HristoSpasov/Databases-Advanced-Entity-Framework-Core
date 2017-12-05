namespace Employees.App.Models
{
    using System.Collections.Generic;
    using System.Text;

    internal class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> Employees { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.FirstName} {this.LastName} | {this.Employees.Count}");

            foreach (EmployeeDto employee in this.Employees)
            {
                sb.AppendLine($"     - {employee.ToString()}");
            }

            return sb.ToString();
        }
    }
}