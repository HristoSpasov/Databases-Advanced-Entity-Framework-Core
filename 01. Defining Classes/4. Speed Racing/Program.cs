namespace _4.Speed_Racing
{
    using System;
    using System.Collections.Generic;
    public class Program
    {
        public static void Main()
        {
            int totalCars = int.Parse(Console.ReadLine());

            Dictionary<string, Car> cars = new Dictionary<string, Car>();

            // Read cars
            for (int i = 0; i < totalCars; i++)
            {
                string[] carArgs = Console.ReadLine().Split();

                Car newCar = new Car(carArgs[0], double.Parse(carArgs[1]), double.Parse(carArgs[2]));

                cars[carArgs[0]] = newCar;
            }

            // Drive the cars
            while(true)
            {
                string cmd = Console.ReadLine();

                if (cmd == "End")
                {
                    break;
                }

                string[] cmdArgs = cmd.Split();

                Car carToDrive = cars[cmdArgs[1]];
                int kmToDrive = int.Parse(cmdArgs[2]);

                if (kmToDrive * carToDrive.FuelConsumptionPerKm > carToDrive.FuelAmount)
                {
                    Console.WriteLine("Insufficient fuel for the drive");

                    continue;
                }

                carToDrive.KilometersTravelled += kmToDrive;
                carToDrive.FuelAmount -= kmToDrive * carToDrive.FuelConsumptionPerKm;
            }

            // Print result
            foreach (var car in cars)
            {
                Console.WriteLine(car.Value);
            }
        }
    }
}
