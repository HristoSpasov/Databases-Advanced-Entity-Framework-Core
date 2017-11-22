namespace _05.Pizza_Calories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings;
        private int totalTopings;
        private double totalCalories;

        public Pizza(string name)
        {
            this.Name = name;
            this.TotalTopings = 0;
            this.dough = new Dough();
            this.toppings = new List<Topping>();
            this.totalCalories = 0;
        }

        public Dough Dough
        {
            set { this.dough = value; }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }

                this.name = value;
            }
        }

        public double TotalCalories
        {
            get { return this.CalculateTotalCalories(); }
        }

        private int TotalTopings
        {
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("Number of toppings should be in range [0..10].");
                }

                this.totalTopings = value;
            }
        }

        public void AddTopping(Topping newTopping)
        {
            this.toppings.Add(newTopping);
            this.TotalTopings = this.toppings.Count();
        }

        private double CalculateTotalCalories()
        {
            return this.dough.Calories + this.toppings.Select(tc => tc.Calories).Sum();
        }
    }
}