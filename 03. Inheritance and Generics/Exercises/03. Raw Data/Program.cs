namespace _03.Raw_Data
{
    using System;
    using System.Linq;
    public class Program
    {
        private static Car[] cars;
        public static void Main()
        {
            // Read total cars count and initialize car array
            ReadCarsCountAndInitialize();

            // Read cars and add to car collection
            ReadCarsAndAddToCarCollection();

            string command = Console.ReadLine();

            // Print cars matching criteria
            PrintAllCarsMatchingCommand(command);
        }

        private static void PrintAllCarsMatchingCommand(string command)
        {
            switch(command)
            {
                case "fragile":
                    {
                        cars.Where(c => c.Cargo.CargoType == "fragile" && c.Tyres.Any(t => t.Pressure < 1)).ToList().ForEach(c => Console.WriteLine(c));
                    }
                    break;
                case "flammable":
                    {
                        cars.Where(c => c.Cargo.CargoType == "flammable" && c.Engine.EnginePower > 250).ToList().ForEach(c => Console.WriteLine(c));
                    }
                    break;
            }
        }

        private static void ReadCarsAndAddToCarCollection()
        {
            for (int i = 0; i < cars.Length; i++)
            {
                string[] args = Console.ReadLine().Split();

                string model = args[0];
                Engine engine = new Engine(int.Parse(args[1]), int.Parse(args[2]));
                Cargo cargo = new Cargo(int.Parse(args[3]), args[4]);
                Tire[] tires =
                {
                    new Tire(double.Parse(args[5]), int.Parse(args[6])),
                    new Tire(double.Parse(args[7]), int.Parse(args[8])),
                    new Tire(double.Parse(args[9]), int.Parse(args[10])),
                    new Tire(double.Parse(args[11]), int.Parse(args[12])),
                };

                Car car = new Car(model, engine, cargo, tires);

                // Add to array
                cars[i] = car;
            }
        }

        private static void ReadCarsCountAndInitialize()
        {
            int count = int.Parse(Console.ReadLine());

            cars = new Car[count];
        }
    }
}
