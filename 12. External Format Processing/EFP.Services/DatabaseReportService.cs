namespace EFP.Services
{
    using System.Linq;
    using System.Text;
    using EFP.Data;
    using EFP.Services.Contracts;

    public class DatabaseReportService : IDatabaseReportService
    {
        private readonly ProductsShopContext context;

        public DatabaseReportService(ProductsShopContext context)
        {
            this.context = context;
        }

        public string GetTableRecordsStatus()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Users table has {this.context.Users.Count()} records.");
            sb.AppendLine($"Products table has {this.context.Products.Count()} records.");
            sb.AppendLine($"Categories table has {this.context.Categories.Count()} records.");

            return sb.ToString().Trim();
        }
    }
}