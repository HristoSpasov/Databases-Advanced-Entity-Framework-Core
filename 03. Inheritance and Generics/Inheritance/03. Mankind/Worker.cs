namespace _03.Mankind
{
    using System;
    public class Worker : Human
    {
        private decimal salary;
        private double workingHoursPerDay;
        public Worker(string firstName, string lastName, decimal salary, double workingHoursPerDay) : base(firstName, lastName)
        {
            this.Salary = salary;
            this.WorkingHoursPerDay = workingHoursPerDay;
        }

        public decimal Salary
        {
            get
            {
                return this.salary;
            }

            private set
            {
                if(value < 10)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
                }

                this.salary = value;
            }
        }

        public double WorkingHoursPerDay
        {
            get
            {
                return this.workingHoursPerDay;
            }

            private set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
                }

                this.workingHoursPerDay = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"Week Salary: {this.salary:F2}" + Environment.NewLine +
                                     $"Hours per day: {this.workingHoursPerDay:F2}" + Environment.NewLine +
                                     $"Salary per hour: {((this.Salary / 5.0m) / (decimal)this.workingHoursPerDay):F2}";
        }
    }
}
