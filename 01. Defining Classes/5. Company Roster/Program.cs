namespace _5.Company_Roster
{
    using System;
    using System.Linq;
    public class Program
    {
        public static void Main()
        {
            int totalEmployees = int.Parse(Console.ReadLine());

            Employee[] employees = new Employee[totalEmployees];

            for (int i = 0; i < totalEmployees; i++)
            {
                string[] args = Console.ReadLine()
                            .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                switch(args.Length)
                {
                    case 6:
                        {
                            Employee empl = new Employee(args[0], decimal.Parse(args[1]), args[2], args[3], args[4], int.Parse(args[5]));
                            employees[i] = empl;
                        }; break;
                    case 4:
                        {
                            Employee empl = new Employee(args[0], decimal.Parse(args[1]), args[2], args[3]);
                            employees[i] = empl;
                        }; break;
                    case 5:
                        {
                            Employee empl = default(Employee);

                            if (int.TryParse(args[args.Length - 1], out int parsed))
                            {
                                empl = new Employee(args[0], decimal.Parse(args[1]), args[2], args[3], int.Parse(args[4]));
                            }
                            else
                            {
                                empl = new Employee(args[0], decimal.Parse(args[1]), args[2], args[3], args[4]);
                            }
                             
                            employees[i] = empl;
                        }; break;

                }
            }

            string departmentWithHighestAverageSalary = employees
                                .GroupBy(d => d.Department)
                                .Select(n => new
                                {
                                    DepartmentName = n.First(),
                                    AvgSalary = n.Average(av => av.Salary)
                                }
                                ).OrderByDescending(av => av.AvgSalary).First().DepartmentName.Department;

            Console.WriteLine($"Highest Average Salary: {departmentWithHighestAverageSalary}");

            // Print result
            foreach (var emp in employees.Where(dn => dn.Department == departmentWithHighestAverageSalary)
                                         .OrderByDescending(s => s.Salary))
                                
            {
                Console.WriteLine(emp);
            }
        }
    }
}
