namespace EFP.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using EFP.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
        {
        }

        public ProductsShopContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Get entities
            Assembly entityAssembly = Assembly.Load("EFP.Models");
            Type[] models = entityAssembly.GetTypes().ToArray();

            // Get models configuration
            Assembly configurationAssembly = Assembly.Load("EFP.ModelsConfiguration");
            Type[] modelsConfiguration = configurationAssembly.GetTypes().ToArray();

            foreach (Type model in models)
            {
                Type correspondingConfiguration = modelsConfiguration.SingleOrDefault(m => m.Name == model.Name + "Configuration");

                if (correspondingConfiguration != null)
                {
                    MethodInfo configre = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Where(m => m.IsGenericMethod && m.Name == "Configure").Single();
                    MethodInfo configGeneric = configre.MakeGenericMethod(new Type[] { model, correspondingConfiguration });
                    configGeneric.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void Configure<TModel, TConfig>(ModelBuilder modelBuilder)
            where TModel : class
            where TConfig : IEntityTypeConfiguration<TModel>
        {
            IEntityTypeConfiguration<TModel> configuration = Activator.CreateInstance<TConfig>();
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}