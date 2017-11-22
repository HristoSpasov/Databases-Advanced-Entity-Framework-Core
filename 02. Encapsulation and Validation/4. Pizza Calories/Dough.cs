namespace _05.Pizza_Calories
{
    using System;
    using System.Collections.Generic;

    internal class Dough
    {
        private static readonly Dictionary<string, double> FlourTypes;
        private static readonly Dictionary<string, double> BakingTechniques;

        private string flourType;
        private string bakingTechnique;
        private int grams;
        private double calories;

        static Dough()
        {
            FlourTypes = new Dictionary<string, double>()
            {
                { "white", 1.5 },
                { "wholegrain", 1.0 }
            };

            BakingTechniques = new Dictionary<string, double>()
            {
                { "crispy", 0.9 },
                { "chewy", 1.1 },
                { "homemade", 1.0 }
            };
        }

        public Dough(string flourType, string bakingTecknique, int grams)
        {
            this.FlourType = flourType;
            this.BakingTechnique = bakingTecknique;
            this.Grams = grams;
            this.calories = this.Grams * FlourTypes[this.FlourType.ToLower()] * BakingTechniques[this.BakingTechnique.ToLower()] * 2;
        }

        public Dough()
        {
        }

        public double Calories
        {
            get { return this.calories; }
        }

        private string FlourType
        {
            get
            {
                return this.flourType;
            }

            set
            {
                if (!FlourTypes.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Invalid type of dough.");
                }

                this.flourType = value;
            }
        }

        private string BakingTechnique
        {
            get
            {
                return this.bakingTechnique;
            }

            set
            {
                if (!BakingTechniques.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Invalid type of dough.");
                }

                this.bakingTechnique = value;
            }
        }

        private int Grams
        {
            get
            {
                return this.grams;
            }

            set
            {
                if (value > 200)
                {
                    throw new ArgumentException($"Dough weight should be in the range [1..200].");
                }

                this.grams = value;
            }
        }
    }
}