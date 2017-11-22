namespace _3.ShopSpree
{
    using System;
    using System.Linq;
    using System.Text;
    using _3.ShopSpree.Entities.Interfaces;
    using _3.ShopSpree.Entities;

    public class Engine
    {
        private StringBuilder output;
        private IDatabase database;
        private bool isRunning;

        public Engine(IDatabase database)
        {
            this.database = database;
            this.output = new StringBuilder();
            this.isRunning = true;
        }

        public void Run()
        {
            if (this.isRunning)
            {
                ReadPersons();
            }

            if (this.isRunning)
            {
                ReadProducts();
            }

            if(this.isRunning)
            {
                StartShopping();
            }

            if(this.isRunning)
            {
                CreteReport();
            }

            Console.WriteLine(this.output.ToString().Trim());
        }

        private void CreteReport()
        {
            foreach (Person person in this.database.Persons)
            {
                this.output.AppendLine($"{person.Name} - {(person.Products.Any() ? string.Join(", ", person.Products) : "Nothing bought")}");
            }
        }

        private void StartShopping()
        {
            while (true)
            {
                string cmd = Console.ReadLine();

                if (cmd == "END")
                {
                    break;
                }

                string[] orederParams = cmd.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                Person person = this.database.GetPerson(orederParams[0]);
                Product product = this.database.GetProduct(orederParams[1]);

                if (person == null || product == null)
                {
                    this.isRunning = false;
                    break;
                }

                if (person.Money < product.Price)
                {
                    this.output.AppendLine($"{person.Name} can't afford {product.Name}");
                }
                else
                {
                    person.AddProduct(product);
                    person.Money -= product.Price;
                    this.output.AppendLine($"{person.Name} bought {product.Name}");
                }
            }
        }

        private void ReadProducts()
        {
            string[] products = Console.ReadLine().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string product in products)
            {
                string[] productArgs = product.Split('=');

                try
                {
                    this.database.AddProduct(new Product(productArgs[0], decimal.Parse(productArgs[1])));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    this.isRunning = false;
                    break;
                }
            }
        }

        private void ReadPersons()
        {
            string[] persons = Console.ReadLine().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string person in persons)
            {
                string[] personArgs = person.Split('=');

                try
                {
                    this.database.AddPerson(new Person(personArgs[0], decimal.Parse(personArgs[1])));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    this.isRunning = false;
                    break;
                }
            }
        }
    }
}
