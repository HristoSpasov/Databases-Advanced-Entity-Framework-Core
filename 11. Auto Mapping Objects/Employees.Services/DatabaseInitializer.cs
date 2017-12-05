namespace Employees.Services
{
    using Employees.Data;
    using Employees.Services.Contracts;

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly EmployeesContext context;

        public DatabaseInitializer(EmployeesContext context)
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