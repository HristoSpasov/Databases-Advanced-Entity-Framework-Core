namespace EFP.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using AutoMapper.QueryableExtensions;
    using EFP.Data;
    using EFP.Models;
    using EFP.Services.Contracts;
    using EFP.Services.ModelImportDto;
    using Newtonsoft.Json;

    public class DatabaseSeedService : IDatabaseSeedService
    {
        private const string UsersJson = @"../JSON/users.json";
        private const string ProductsJson = @"../JSON/products.json";
        private const string CategoriesJson = @"../JSON/categories.json";

        private const string UsersXml = @"../XML/users.xml";
        private const string ProductsXml = @"../XML/products.xml";
        private const string CategoriesXml = @"../XML/categories.xml";

        private readonly ProductsShopContext context;

        public DatabaseSeedService(ProductsShopContext context)
        {
            this.context = context;
        }

        public void SeedFromJson()
        {
            // Users import
            string users = File.ReadAllText(UsersJson, Encoding.UTF8);
            UserImportDto[] usersImport = JsonConvert.DeserializeObject<UserImportDto[]>(users);
            User[] userArr = usersImport.AsQueryable().ProjectTo<User>().ToArray();
            this.context.Users.AddRange(userArr);
            this.context.SaveChanges();

            // Categories import
            string categories = File.ReadAllText(CategoriesJson, Encoding.UTF8);
            CategoryImportDto[] categoriesImport = JsonConvert.DeserializeObject<CategoryImportDto[]>(categories);
            Category[] categoryArr = categoriesImport.AsQueryable().ProjectTo<Category>().ToArray();
            this.context.Categories.AddRange(categoryArr);
            this.context.SaveChanges();

            // Products import
            string products = File.ReadAllText(ProductsJson, Encoding.UTF8);
            ProductImportDto[] productsImport = JsonConvert.DeserializeObject<ProductImportDto[]>(products);
            Product[] productArr = productsImport.AsQueryable().ProjectTo<Product>().ToArray();

            // Add random seller/buyer data to products
            this.AddRandomSellerToProducts(userArr, productArr);
            this.AddRandomBuyerToProducts(userArr, productArr);
            this.context.Products.AddRange(productArr);
            this.context.SaveChanges();

            // Random map each product to category
            CategoryProduct[] categoryProduct = this.MapProductToCategory(productArr, categoryArr);
            this.context.CategoryProducts.AddRange(categoryProduct);
            this.context.SaveChanges();
        }

        public void SeedFromXml()
        {
            // Users import
            string users = File.ReadAllText(UsersXml, Encoding.UTF8);
            XDocument usersXml = XDocument.Parse(users);
            UserImportDto[] usersImport = usersXml.Root.Elements()
                                                  .Select(u => new UserImportDto
                                                  {
                                                      FirstName = u.Attribute("firstName")?.Value,
                                                      LastName = u.Attribute("lastName").Value,
                                                      Age = u.Attribute("age") != null ? int.Parse(u.Attribute("age").Value) : default(int?)
                                                  })
                                                  .ToArray();
            User[] userArr = usersImport.AsQueryable().ProjectTo<User>().ToArray();
            this.context.Users.AddRange(userArr);
            this.context.SaveChanges();

            // Categories import
            string categories = File.ReadAllText(CategoriesXml, Encoding.UTF8);
            XDocument categoriesXml = XDocument.Parse(categories);
            CategoryImportDto[] categoriesImport = categoriesXml.Root.Elements()
                                                                .Select(c => new CategoryImportDto
                                                                {
                                                                    Name = c.Element("name").Value
                                                                })
                                                                .ToArray();

            Category[] categoryArr = categoriesImport.AsQueryable().ProjectTo<Category>().ToArray();
            this.context.Categories.AddRange(categoryArr);
            this.context.SaveChanges();

            // Products import
            string products = File.ReadAllText(ProductsXml, Encoding.UTF8);
            XDocument productsXml = XDocument.Parse(products);
            ProductImportDto[] productsImport = productsXml.Root.Elements()
                                                           .Select(p => new ProductImportDto
                                                           {
                                                               Name = p.Element("name").Value,
                                                               Price = decimal.Parse(p.Element("price").Value)
                                                           })
                                                           .ToArray();
            Product[] productArr = productsImport.AsQueryable().ProjectTo<Product>().ToArray();

            // Add random seller/buyer data to products
            this.AddRandomSellerToProducts(userArr, productArr);
            this.AddRandomBuyerToProducts(userArr, productArr);
            this.context.Products.AddRange(productArr);
            this.context.SaveChanges();

            // Random map each product to category
            CategoryProduct[] categoryProduct = this.MapProductToCategory(productArr, categoryArr);
            this.context.CategoryProducts.AddRange(categoryProduct);
            this.context.SaveChanges();
        }

        private void AddRandomSellerToProducts(User[] userArr, Product[] productArr)
        {
            Random rnd = new Random();

            for (int i = 0; i < productArr.Length; i++)
            {
                productArr[i].SellerId = userArr[rnd.Next(0, userArr.Length)].Id;
            }
        }

        private void AddRandomBuyerToProducts(User[] userArr, Product[] productArr)
        {
            Random rnd = new Random();

            for (int i = 0; i < productArr.Length; i += 2)
            {
                productArr[i].BuyerId = userArr[rnd.Next(0, userArr.Length)].Id;
            }
        }

        private CategoryProduct[] MapProductToCategory(Product[] productArr, Category[] categoryArr)
        {
            Random rnd = new Random();

            CategoryProduct[] cp = new CategoryProduct[productArr.Length];

            for (int i = 0; i < cp.Length; i++)
            {
                CategoryProduct newCp = new CategoryProduct()
                {
                    ProductId = productArr[i].Id,
                    CategoryId = categoryArr[rnd.Next(0, categoryArr.Length)].Id
                };

                cp[i] = newCp;
            }

            return cp;
        }
    }
}