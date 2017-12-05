namespace EFP.Services
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using EFP.Data;
    using EFP.Services.Contracts;
    using EFP.Services.ModelExportDto;
    using Newtonsoft.Json;

    public class DataExportService : IDataExportService
    {
        private readonly ProductsShopContext context;

        public DataExportService(ProductsShopContext context)
        {
            this.context = context;
        }

        public void ExportToJson()
        {
            // Query 1
            string productsInRange = JsonConvert.SerializeObject(this.GetProductsInRangeJson(), Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText(@"../Query 1 ProductsInRangeJson.json", productsInRange);
            Process.Start("notepad.exe", @"../Query 1 ProductsInRangeJson.json");

            // Query 2
            string successfullySoldProducts = JsonConvert.SerializeObject(this.GetSuccessfullySoldProductsJson(), Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText(@"../Query 2 SuccessfullySoldProducts.json", successfullySoldProducts);
            Process.Start("notepad.exe", @"../Query 2 SuccessfullySoldProducts.json");

            // Query 3
            string categoriesByProductsCount = JsonConvert.SerializeObject(this.GetCategoriesByProductsCountJson(), Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText(@"../Query 3 CategoriesByProductsCount.json", categoriesByProductsCount);
            Process.Start("notepad.exe", @"../Query 3 CategoriesByProductsCount.json");

            // Query 4
            string usersAndProducts = JsonConvert.SerializeObject(this.GetUsersAndProductsJsonXml(), Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText(@"../Query 4 UsersAndProducts.json", usersAndProducts);
            Process.Start("notepad.exe", @"../Query 4 UsersAndProducts.json");
        }

        public void ExportToXml()
        {
            // Query 1
            ProductsInRangeDto[] productsInRange = this.GetProductsInRangeXml();

            XDocument productsXml = new XDocument(new XElement("products"));

            foreach (var p in productsInRange)
            {
                productsXml.Root.Add(new XElement("product", new XAttribute("name", p.Name), new XAttribute("price", p.Price), new XAttribute("buyer", p.Seller)));
            }

            File.WriteAllText(@"../Query 1 ProductsInRangeXml.xml", productsXml.ToString());
            Process.Start("notepad.exe", @"../Query 1 ProductsInRangeXml.xml");

            // Query 2
            SoldProductsXmlDto[] soldProducts = this.GetSoldProductsXML();
            XDocument soldProductsXml = new XDocument(new XElement("users"));

            foreach (var p in soldProducts)
            {
                var sold = new XElement("sold-products");
                foreach (var pr in p.SoldProducts)
                {
                    sold.Add(new XElement("product", new XElement("name", pr.Name), new XElement("price", pr.Price)));
                }

                soldProductsXml.Root.Add(new XElement("user", p.FirstName != null ? new XAttribute("first-name", p.FirstName) : null, new XAttribute("last-name", p.LastName), sold));
            }

            File.WriteAllText(@"../Query 2 SoldProductsXml.xml", soldProductsXml.ToString());
            Process.Start("notepad.exe", @"../Query 2 SoldProductsXml.xml");

            // Query 3
            CategoriesByProductsCountXml[] catByProductsCount = this.GetCategoriesByProductsCountXml();
            XDocument catByProductsCountXml = new XDocument(new XElement("categories"));

            foreach (var c in catByProductsCount)
            {
                catByProductsCountXml.Root.Add(new XElement("category", new XAttribute("name", c.Category), new XElement("products-count", c.ProductsCount), new XElement("average-prive", c.AveragePrice), new XElement("total-revenue", c.TotalRevenue)));
            }

            File.WriteAllText(@"../Query 3 CategoriesByProductsCountXml.xml", catByProductsCountXml.ToString());
            Process.Start("notepad.exe", @"../Query 3 CategoriesByProductsCountXml.xml");

            // Query 4
            UsersAndProductsDto usersAndProducts = this.GetUsersAndProductsJsonXml();
            XDocument usersAndProductsXml = new XDocument(new XElement("users", new XAttribute("count", usersAndProducts.UsersCount)));

            foreach (var u in usersAndProducts.Users)
            {
                XAttribute firstName = u.FirstName != null ? new XAttribute("first-name", u.FirstName) : null;
                XAttribute lastName = new XAttribute("last-name", u.LastName);
                XAttribute age = u.Age != null ? new XAttribute("age", u.Age) : null;

                XElement user = new XElement("user", firstName, lastName, age);

                XElement productsSold = new XElement("sold-products", new XAttribute("count", u.SoldProducts.Count));
                foreach (var pr in u.SoldProducts.Products)
                {
                    XElement currProd = new XElement("product", new XAttribute("name", pr.Name), new XAttribute("price", pr.Price));
                    productsSold.Add(currProd);
                }

                user.Add(productsSold);

                usersAndProductsXml.Root.Add(user);
            }

            File.WriteAllText(@"../Query 4 UsersAndProducts.xml", usersAndProductsXml.ToString());
            Process.Start("notepad.exe", @"../Query 4 UsersAndProducts.xml");
        }

        private SoldProductsXmlDto[] GetSoldProductsXML()
        {
            return this.context
                       .Users
                       .Where(s => s.ProductsSold.Any())
                       .OrderBy(ln => ln.LastName)
                       .ThenBy(fn => fn.FirstName)
                       .Select(p => new SoldProductsXmlDto
                       {
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           SoldProducts = p.ProductsSold
                                           .Select(ps => new ProductDto
                                           {
                                               Name = ps.Name,
                                               Price = ps.Price,
                                           }).ToArray()
                       })
                       .ToArray();
        }

        private CategoriesByProductsCountXml[] GetCategoriesByProductsCountXml()
        {
            return this.context
                       .Categories
                       .Select(p => new CategoriesByProductsCountXml
                       {
                           Category = p.Name,
                           ProductsCount = p.CategoryProducts.Where(cId => cId.CategoryId == p.Id).Count(),
                           AveragePrice = p.CategoryProducts
                                           .Where(cId => cId.CategoryId == p.Id)
                                           .Select(pp => pp.Product.Price).Sum() / p.CategoryProducts.Where(cId => cId.CategoryId == p.Id).Count(),
                           TotalRevenue = p.CategoryProducts
                                           .Where(cId => cId.CategoryId == p.Id)
                                           .Select(pp => pp.Product.Price)
                                           .Sum()
                       })
                       .OrderBy(p => p.ProductsCount)
                       .ToArray();
        }

        private ProductsInRangeDto[] GetProductsInRangeXml()
        {
            return this.context
                       .Products
                       .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                       .Select(p => new ProductsInRangeDto
                       {
                           Name = p.Name,
                           Price = p.Price,
                           Seller = $"{p.Buyer.FirstName} {p.Buyer.LastName}".Trim()
                       })
                       .OrderBy(p => p.Price)
                       .ToArray();
        }

        private UsersAndProductsDto GetUsersAndProductsJsonXml()
        {
            return this.context
                       .Users
                       .Where(u => u.ProductsSold.Any())
                       .Select(u => new UsersAndProductsDto
                       {
                           UsersCount = this.context
                                            .Users
                                            .Where(us => us.ProductsSold.Any())
                                            .Count(),
                           Users = this.context
                                       .Users
                                       .Where(us => us.ProductsSold.Any())
                                       .OrderByDescending(pc => pc.ProductsSold.Count())
                                       .ThenBy(ln => ln.LastName)
                                       .Select(us => new UserDto
                                       {
                                           FirstName = us.FirstName,
                                           LastName = us.LastName,
                                           Age = us.Age,
                                           SoldProducts = us.ProductsSold
                                                            .Select(p => new SoldProductsDto
                                                            {
                                                                Count = us.ProductsSold.Count,
                                                                Products = us.ProductsSold
                                                                             .Select(pr => new ProductDto
                                                                             {
                                                                                 Name = pr.Name,
                                                                                 Price = pr.Price
                                                                             }).ToArray()
                                                            }).FirstOrDefault()
                                       }).ToArray()
                       }).First();
        }

        private CategoriesByProductsDto[] GetCategoriesByProductsCountJson()
        {
            return this.context
                       .Categories
                       .OrderBy(c => c.Name)
                       .Select(p => new CategoriesByProductsDto
                       {
                           Category = p.Name,
                           ProductsCount = p.CategoryProducts.Where(cId => cId.CategoryId == p.Id).Count(),
                           AveragePrice = p.CategoryProducts
                                           .Where(cId => cId.CategoryId == p.Id)
                                           .Select(pp => pp.Product.Price).Sum() / p.CategoryProducts.Where(cId => cId.CategoryId == p.Id).Count(),
                           TotalRevenue = p.CategoryProducts
                                           .Where(cId => cId.CategoryId == p.Id)
                                           .Select(pp => pp.Product.Price)
                                           .Sum()
                       })
                       .ToArray();
        }

        private ProductsInRangeDto[] GetProductsInRangeJson()
        {
            return this.context
                       .Products
                       .Where(p => p.Price >= 500 && p.Price <= 1000)
                       .Select(p => new ProductsInRangeDto
                       {
                           Name = p.Name,
                           Price = p.Price,
                           Seller = $"{p.Seller.FirstName} {p.Seller.LastName}".Trim()
                       })
                       .OrderBy(p => p.Price)
                       .ToArray();
        }

        private SuccessfullySoldProductsDto[] GetSuccessfullySoldProductsJson()
        {
            return this.context
                       .Users
                       .Where(s => s.ProductsSold.Any(b => b.Buyer != null))
                       .OrderBy(fn => fn.FirstName)
                       .ThenBy(ln => ln.LastName)
                       .Select(p => new SuccessfullySoldProductsDto
                       {
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           SoldProducts = p.ProductsSold
                                           .Select(ps => new ProductExportDto
                                           {
                                               Name = ps.Name,
                                               Price = ps.Price,
                                               BuyerFirstName = ps.Buyer.FirstName,
                                               BuyerLastName = ps.Buyer.LastName
                                           }).ToArray()
                       })
                       .ToArray();
        }
    }
}