namespace TeamBuildere.Services
{
    using TeamBuilder.Data;
    using TeamBuildere.Services.Contracts;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly TeamBuilderContext context;

        public DatabaseInitializerService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public void DatabaseInitialize()
        {
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }
    }
}