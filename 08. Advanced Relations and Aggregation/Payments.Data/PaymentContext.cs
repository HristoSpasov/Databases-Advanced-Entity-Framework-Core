namespace Payments.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Payments.Entities;

    public class PaymentContext : DbContext
    {
        public PaymentContext()
        {
        }

        public PaymentContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            string configurationString = "Configuration";

            // Get all entities types
            Assembly entityAssembly = Assembly.Load("Payments.Entities");
            Type[] entities = entityAssembly.GetTypes();

            // Get configuration entities tyoes
            Type[] entitiesConfig = Assembly.GetExecutingAssembly()
                                            .GetTypes()
                                            .Where(ns => ns.Namespace == "Payments.Data.EntityConfig")
                                            .ToArray();

            // Loop over entities and configure
            foreach (var entity in entities)
            {
                Type entityConfig = entitiesConfig.FirstOrDefault(ec => ec.Name == entity.Name + configurationString);

                if (entityConfig != null)
                {
                    MethodInfo configureMethod = typeof(PaymentContext).GetMethod("Configure", BindingFlags.Instance | BindingFlags.NonPublic);
                    MethodInfo genericConfig = configureMethod.MakeGenericMethod(entity, entityConfig);

                    genericConfig.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void Configure<TEntity, TEntityConfig>(ModelBuilder builder)
            where TEntity : class
            where TEntityConfig : IEntityTypeConfiguration<TEntity>
        {
            IEntityTypeConfiguration<TEntity> configuration = Activator.CreateInstance<TEntityConfig>();
            builder.ApplyConfiguration(configuration);
        }
    }
}