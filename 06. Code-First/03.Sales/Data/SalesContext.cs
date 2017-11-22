namespace P03_SalesDatabase.Data
{
    using P03_SalesDatabase.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConfigurationString);
            }
        }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customers description and validation
            builder.Entity<Customer>()
                .HasKey(k => k.CustomerId);

            builder.Entity<Customer>()
                .HasMany(s => s.Sales)
                .WithOne(c => c.Customer)
                .HasForeignKey(fk => fk.CustomerId);

            builder.Entity<Customer>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(100)");

            builder.Entity<Customer>()
                .Property(p => p.Email)
                .HasColumnType("varchar(80)");

            // Stores description and validation
            builder.Entity<Store>()
                .HasKey(k => k.StoreId);

            builder.Entity<Store>()
                .HasMany(s => s.Sales)
                .WithOne(s => s.Store)
                .HasForeignKey(fk => fk.StoreId);

            builder.Entity<Store>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(80)");

            // Products description and validation
            builder.Entity<Product>()
                .HasKey(k => k.ProductId);

            builder.Entity<Product>()
                .HasMany(s => s.Sales)
                .WithOne(s => s.Product)
                .HasForeignKey(fk => fk.ProductId);

            builder.Entity<Product>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(50)");

            builder.Entity<Product>()
                .Property(p => p.Description)
                .HasColumnType("nvarchar(250)")
                .HasDefaultValue("No description");

            // Sales description and validation
            builder.Entity<Sale>()
                .HasKey(k => k.SaleId);

            builder.Entity<Sale>()
                .HasOne(c => c.Customer)
                .WithMany(s => s.Sales);

            builder.Entity<Sale>()
                .HasOne(p => p.Product)
                .WithMany(s => s.Sales);

            builder.Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Sales);

            builder.Entity<Sale>()
                .Property(p => p.Date)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
