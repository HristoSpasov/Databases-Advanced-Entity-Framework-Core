namespace _5.Company_Roster
{
    public class Employee
    {
        public Employee(string name, decimal salary, string position, string department)
        {
            this.Name = name;
            this.Salary = salary;
            this.Position = position;
            this.Department = department;
            this.Email = "n/a";
            this.Age = -1;
        }

        public Employee(string name, decimal salary, string position, string department, string email, int age)
            : this(name, salary, position, department)
        {
            this.Email = email;
            this.Age = age;
        }

        public Employee(string name, decimal salary, string position, string department, string email)
           : this(name, salary, position, department)
        {
            this.Email = email;
            this.Age = -1;
        }

        public Employee(string name, decimal salary, string position, string department, int age)
            : this(name, salary, position, department)
        {
            this.Age = age;
            this.Email = "n/a";
        }

        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Salary:F2} {this.Email} {this.Age}";
        }
    }
}
