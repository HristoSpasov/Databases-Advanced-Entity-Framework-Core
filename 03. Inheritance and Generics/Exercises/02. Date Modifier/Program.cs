namespace _02.Date_Modifier
{
    using System;
    public class Program
    {
        public static void Main()
        {
            string start = Console.ReadLine();
            string end = Console.ReadLine();

            DateModifier modifier = new DateModifier(start, end);

            Console.WriteLine(modifier.DifferenceBwtweenTwoDates());
        }
    }
}
