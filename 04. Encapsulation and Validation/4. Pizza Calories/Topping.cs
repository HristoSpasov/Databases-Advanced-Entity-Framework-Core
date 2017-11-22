namespace _05.Pizza_Calories
{
    using System;
    using System.Collections.Generic;

    internal class Topping
    {
        private static readonly Dictionary<string, double> ToppingTypes;

        private string type;
        private int grams;
        private double calories;

        static Topping()
        {
            ToppingTypes = new Dictionary<string, double>()
            {
                { "meat", 1.2 },
                { "veggies", 0.8 },
                { "cheese", 1.1 },
                { "sauce", 0.9 }
            };
        }

        public Topping(string type, int grams)
        {
            this.Type = type;
            this.Grams = grams;
            this.calories = this.Grams * ToppingTypes[this.Type.ToLower()] * 2;
        }

        public double Calories
        {
            get { return this.calories; }
        }

        private int Grams
        {
            get
            {
                return this.grams;
            }

            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.Type} weight should be in the range [1..50].");
                }

                this.grams = value;
            }
        }

        private string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                if (!ToppingTypes.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                this.type = value;
            }
        }
    }
}