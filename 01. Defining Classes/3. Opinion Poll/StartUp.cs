namespace _3.Opinion_Poll
{
    using System;
    using System.Linq;
    public class StartUp
    {
        private static Person[] persons;
        public static void Main()
        {
            // Total persons
            int personsToRead = int.Parse(Console.ReadLine());

            // Initialize person array
            persons = new Person[personsToRead];

            // Read person data
            for (int i = 0; i < personsToRead; i++)
            {
                string[] personArgs = Console.ReadLine()
                                       .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                Person newPerson = new Person(personArgs[0], int.Parse(personArgs[1]));

                persons[i] = newPerson;
            }

            // Print result
            foreach (var person in persons.Where(p => p.Age > 30).OrderBy(pn => pn.Name))
            {
                Console.WriteLine(person);
            }
        }
    }
}
