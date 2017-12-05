namespace Employees.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Employees.Models;
    using Microsoft.EntityFrameworkCore;

    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
        {
        }

        public EmployeesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string modelNamespace = "Emplpoyees.Models";
            string configNamespace = "Employees.ModelsConfig";

            Type[] models = Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .Where(ns => ns.Namespace == modelNamespace)
                                    .ToArray();

            Type[] configs = Assembly.GetExecutingAssembly()
                                     .GetTypes()
                                     .Where(ns => ns.Namespace == configNamespace)
                                     .ToArray();

            foreach (Type model in models)
            {
                Type configType = configs.SingleOrDefault(n => n.Name == model.Name + "Configuration");

                if (configType != null)
                {
                    MethodInfo configureMethod = this.GetType()
                                            .GetMethod("Configure", BindingFlags.Instance | BindingFlags.NonPublic);

                    MethodInfo genericConfigure = configureMethod.MakeGenericMethod(new[] { model, configType });
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