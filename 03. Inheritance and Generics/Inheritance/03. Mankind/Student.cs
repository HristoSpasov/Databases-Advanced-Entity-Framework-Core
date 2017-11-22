namespace _03.Mankind
{
    using System;
    using System.Text.RegularExpressions;
    public class Student : Human
    {
        private string facultyNumber;

        public Student(string firstName, string lastName, string facultyNumber) : base(firstName, lastName)
        {
            this.FacultyNumber = facultyNumber;
        }

        public string FacultyNumber
        {
            get
            {
                return this.facultyNumber;
            }

            private set
            {
                Regex reg = new Regex(@"^[A-Za-z0-9]{5,10}$");

                if(!reg.IsMatch(value))
                {
                    throw new ArgumentException("Invalid faculty number!");
                }

                this.facultyNumber = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"Faculty number: {this.FacultyNumber}";
        }
    }
}
