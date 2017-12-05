namespace PhotoShare.Services
{
    using PhotoShare.Data;
    using PhotoShare.Services.Interfaces;

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly PhotoShareContext context;

        public DatabaseInitializer(PhotoShareContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.EnsureDeleted();

            this.context.Database.EnsureCreated();
        }
    }
}