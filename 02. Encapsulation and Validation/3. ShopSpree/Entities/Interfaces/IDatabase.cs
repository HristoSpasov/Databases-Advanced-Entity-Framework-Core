using System.Collections.Generic;

namespace _3.ShopSpree.Entities.Interfaces
{
    public interface IDatabase
    {
        void AddProduct(Product product);
        Product GetProduct(string name);
        void AddPerson(Person person);
        Person GetPerson(string name);
        void AddProductToPersonBasket(Person person, Product product);
        IReadOnlyCollection<Person> Persons { get; }
    }
}
