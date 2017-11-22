using System;
using System.Text;

namespace P03_SalesDatabase
{
    public static class SeedData
    {
        public const int CreditCardStringLength = 10;

        public const int NumbersToUSeForPricaCalculation = 3;

        public const int DefaultSalesToGenerate = 100;

        public static string[] CustomersNames = new string[] { "Ivan", "Gosho", "Pesho", "Ivan", "Mariq", "Gergana", "Ivanka", "Pepi", "Marry", "Steve", "Bill", "Jason", "Silvester", "Tom", "Jerry" };

        public static string[] CustomersEmailDomain = new string[] { "@yahoo.com", "@abv.bg", "@mail.ru", "@gmail.com", "@yahoo.co.uk", "@softuni.bg", "@mail.bg", "@thepiratebay.se"};

        public static string[] ProductsName = new string[] { "Nail", "Tomato", "Milk", "Water", "Bread", "Vodka", "Rakia", "Bira", "Cheese", "Salad", "Hammer", "Tesla", "Screwdriver" };

        public static int[] ProductsQuantity = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public static string[] ProductsDescriptions = new string[] { null, "This one is the best", "Very cool", "Buy and try", "Nice product", "Middle class product", "Entry level", "Very delicious", "The best drink ever" };

        public static string[] StoresName = new string[] { "Mike's store", "Store One", "Mega store", "Local store", "Europe store", "Infinite store", "Pirate store", "OMFG Store" };
    }
}
