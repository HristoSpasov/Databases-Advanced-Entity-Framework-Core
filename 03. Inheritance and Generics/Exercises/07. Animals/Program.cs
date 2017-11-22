namespace _07.Animals
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using _07.Animals.Entities;

    public class Program
    {
        private const string InputValidatorPattern = @"^([^\s]+)\s+([0-9]+)\s*([^\s]+)?$";

        public static void Main()
        {
            // Output store
            StringBuilder output = new StringBuilder();

            while (true)
            {
                try
                {
                    string line = Console.ReadLine();

                    if (line == "Beast!")
                    {
                        break;
                    }

                    string animalData = Console.ReadLine();

                    // Validate if all tokens are present
                    if (!Regex.IsMatch(animalData, InputValidatorPattern))
                    {
                        throw new ArgumentException("Invalid input!");
                    }

                    Match match = Regex.Match(animalData, InputValidatorPattern);

                    string name = match.Groups[1].Value;
                    int age = int.Parse(match.Groups[2].Value);
                    string gender = match.Groups[3].Value;

                    // If no gender => check if type is Tomcat or Kitten
                    if (string.IsNullOrWhiteSpace(gender))
                    {
                        if (line == typeof(Tomcat).Name)
                        {
                            gender = "Male";
                        } 
                        else if (line == typeof(Kitten).Name)
                        {
                            gender = "Female";
                        }
                    }

                    // Animal constructor params
                    object[] constructorParams = new object[]
                    {
                    name, age, gender
                    };

                    Type toCreate = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(n => n.Name == line);

                    // Check for inexisting animal type
                    if(toCreate == null)
                    {
                        throw new ArgumentException("Invalid input!");
                    }

                    // Make animal instance
                    Animal animal = (Animal)Activator.CreateInstance(toCreate, constructorParams);

                    // Append to output
                    output.AppendLine(toCreate.Name);
                    output.AppendLine(animal.ToString());
                    output.AppendLine(animal.ProduceSound());
                }
                catch (Exception e)
                {
                    output.AppendLine(e.Message);
                }
            }

            // Print result
            Console.Write(output.ToString());
        }
    }
}
