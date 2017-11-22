namespace _01.Oldest_Family_Member
{
    using System;
    using System.Reflection;
    public class Program
    {
        public static void Main()
        {
            MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
            MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
            if (oldestMemberMethod == null || addMemberMethod == null)
            {
                throw new Exception();
            }

            Family family = new Family();

            int members = int.Parse(Console.ReadLine());

            for (int i = 0; i < members; i++)
            {
                string[] personArgs = Console.ReadLine().Split();
                
                family.AddMember(new Person(personArgs[0], int.Parse(personArgs[1])));
            }

            Console.WriteLine(family.GetOldestMember());
        }
    }
}
