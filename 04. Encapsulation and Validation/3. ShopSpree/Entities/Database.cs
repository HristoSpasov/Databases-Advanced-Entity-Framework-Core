namespace _3.ShopSpree.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Interfaces;
    public class Database : IDatabase
    {
        private List<Product> products;
        private List<Person> persons;

        public Database()
        {
            this.products = new List<Product>();
            this.persons = new List<Person>();
        }

        public IReadOnlyCollection<Person> Persons => this.persons;

        public void AddPerson(Person person)
        {
            this.persons.Add(person);
        }

        public void AddProduct(Product product)
        {
            this.products.Add(product);
        }

        public void AddProductToPersonBasket(Person person, Product product)
        {
            this.persons.FirstOrDefault(p => p.Name == person.Name).AddProduct(product);
        }

        public Person GetPerson(string name)
        {
            return this.persons.FirstOrDefault(p => p.Name == name);
        }

        public Product GetProduct(string name)
        {
            return this.products.FirstOrDefault(pr => pr.Name == name);
        }
    }
}
