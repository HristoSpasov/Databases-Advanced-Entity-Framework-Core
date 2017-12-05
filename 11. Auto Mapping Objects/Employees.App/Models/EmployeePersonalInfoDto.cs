namespace Employees.App.Models
{
    using System;

    internal class EmployeePersonalInfoDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id} - {this.FirstName} {this.LastName} - ${this.Salary:F2}{Environment.NewLine}" +
                $"Birthday: {(this.Birthday.HasValue ? this.Birthday.Value.ToString("dd-MM-yyyy") : "No birthday is set!")}{Environment.NewLine}" +
                $"Address: {this.Address ?? "No address is set!"}";
        }
    }
}