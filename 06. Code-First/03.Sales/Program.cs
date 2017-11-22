namespace P03_SalesDatabase
{
    using System;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data;
    using P03_SalesDatabase.Data.Models;

    public class Program
    {
        private static SalesContext context;
        private static int salesCountToGenerate;

        static Program()
        {
            context = new SalesContext();
            salesCountToGenerate = SeedData.DefaultSalesToGenerate;
        }

        public static void Main()
        {
            PromptForCustomSalesCount();

            ResetDatabaseRoutine();
        }

        private static void ResetDatabaseRoutine()
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Console.WriteLine("Seeding data please wait...");
            Seed();
            Console.WriteLine("Succesfully seeded database.");
        }

        private static void Seed()
        {
            Random rnd = new Random();

            decimal[] ProductsPrices = PriceArrayGenerator();
            string[] CustomersCreditCardNumber = CreditCardArrayGenerator();

            // Generate Customers, Stores, Products
            for (int i = 1; i <= salesCountToGenerate; i++)
            {
                Customer newCustomer = new Customer()
                {
                    CreditCardNumber = CustomersCreditCardNumber[rnd.Next(0, CustomersCreditCardNumber.Length)],
                    Name = SeedData.CustomersNames[rnd.Next(0, SeedData.CustomersNames.Length)]
                };

                newCustomer.Email = $"{newCustomer.Name} + {SeedData.CustomersEmailDomain[rnd.Next(0, SeedData.CustomersEmailDomain.Length)]}";

                Store newStore = new Store()
                {
                    Name = SeedData.StoresName[rnd.Next(0, SeedData.StoresName.Length)]
                };

                Product newProduct = new Product()
                {
                    Name = SeedData.ProductsName[rnd.Next(0, SeedData.ProductsName.Length)],
                    Price = ProductsPrices[rnd.Next(0, ProductsPrices.Length)],
                    Quantity = SeedData.ProductsQuantity[rnd.Next(0, SeedData.ProductsQuantity.Length)],
                    Description = SeedData.ProductsDescriptions[rnd.Next(0, SeedData.ProductsDescriptions.Length)]
                };

                context.Customers.Add(newCustomer);
                context.Stores.Add(newStore);
                context.Products.Add(newProduct);
            }

            context.SaveChanges();

            // Generate Sales
            for (int i = 1; i <= salesCountToGenerate; i++)
            {
                Sale newSale = new Sale()
                {
                    CustomerId = rnd.Next(1, salesCountToGenerate),
                    ProductId = rnd.Next(1, salesCountToGenerate),
                    StoreId = rnd.Next(1, salesCountToGenerate)
                };

                context.Sales.Add(newSale);
            }

            context.SaveChanges();
        }

        private static void PromptForCustomSalesCount()
        {
            Console.Write($"Enter positive integer value. Leave empty for default ({SeedData.DefaultSalesToGenerate}) > ");
            string input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input))
            {
                if(int.TryParse(input, out int parsed))
                {
                    salesCountToGenerate = parsed;
                    Console.WriteLine($"Default sales to be generated value was changed to {salesCountToGenerate}!");
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    PromptForCustomSalesCount();
                }
            }
        }

        private static decimal[] PriceArrayGenerator()
        {
            Random rnd = new Random();

            int totalPricesToGenerate = SeedData.ProductsName.Length;
            decimal[] prices = new decimal[totalPricesToGenerate];

            int[] numbersToUse = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < totalPricesToGenerate; i++)
            {
                decimal currentPrice = default(decimal);

                for (int j = 0; j < SeedData.NumbersToUSeForPricaCalculation; j++)
                {
                    currentPrice += numbersToUse[rnd.Next(0, numbersToUse.Length - 1)];
                }

                prices[i] = currentPrice * (decimal)Math.PI;
            }

            return prices;
        }

        private static string[] CreditCardArrayGenerator()
        {
            Random rnd = new Random();

            int creditCardsToGenerate = salesCountToGenerate;

            string[] creditCards = new string[creditCardsToGenerate];

            char[] symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            for (int i = 0; i < creditCardsToGenerate; i++)
            {
                StringBuilder currentCard = new StringBuilder();

                for (int j = 0; j < SeedData.CreditCardStringLength; j++)
                {
                    currentCard.Append(symbols[rnd.Next(0, symbols.Length - 1)]);
                }

                creditCards[i] = currentCard.ToString();
            }

            return creditCards;
        }
    }
}
