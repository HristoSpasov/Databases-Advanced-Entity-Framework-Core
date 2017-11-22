namespace _05.Pizza_Calories
{
    using System;

    internal class Program
    {
        private static void Main()
        {
            try
            {
                string line = Console.ReadLine();

                if (line.StartsWith("Pizza"))
                {
                    string[] pizzaLine = line.Split();
                    string[] doughLine = Console.ReadLine().Split();
                    Pizza newPizza = new Pizza(pizzaLine[1]);

                    // Read all toppings
                    while (true)
                    {
                        string cmd = Console.ReadLine();

                        if (cmd == "END")
                        {
                            break;
                        }

                        string[] toppingLine = cmd.Split();

                        newPizza.AddTopping(new Topping(toppingLine[1], int.Parse(toppingLine[2])));
                    }

                    newPizza.Dough = new Dough(doughLine[1], doughLine[2], int.Parse(doughLine[3]));

                    Console.WriteLine($"{newPizza.Name} - {newPizza.TotalCalories:f2} Calories.");
                }

                if (line.StartsWith("Dough"))
                {
                    string[] doughLine = line.Split();

                    Dough newDough = new Dough(doughLine[1], doughLine[2], int.Parse(doughLine[3]));

                    Console.WriteLine($"{newDough.Calories:F2}");

                    while (true)
                    {
                        string cmd = Console.ReadLine();

                        if (cmd == "END")
                        {
                            break;
                        }

                        string[] toppingLine = cmd.Split();

                        Topping newTopping = new Topping(toppingLine[1], int.Parse(toppingLine[2]));

                        Console.WriteLine($"{newTopping.Calories:F2}");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}