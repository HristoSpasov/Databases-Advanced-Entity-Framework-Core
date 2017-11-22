namespace _03.Mankind
{
    using System;
    public class Program
    {
        public static void Main()
        {
            try
            {
                string[] student = Console.ReadLine().Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string[] worker = Console.ReadLine().Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                Student st = new Student(student[0], student[1], student[2]);
                Worker w = new Worker(worker[0], worker[1], decimal.Parse(worker[2]), double.Parse(worker[3]));

                Console.WriteLine(st);
                Console.WriteLine();
                Console.WriteLine(w);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
