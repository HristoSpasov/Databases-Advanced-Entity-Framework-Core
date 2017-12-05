namespace EFP.Services
{
    using EFP.Data;
    using EFP.Services.Contracts;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly ProductsShopContext context;

        public DatabaseInitializerService(ProductsShopContext context)
        {
            this.context = context;
        }

        public void InitializeDb()
        {
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }
    }
}