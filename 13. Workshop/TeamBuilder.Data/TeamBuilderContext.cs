namespace TeamBuilder.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Models;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        {
        }

        public TeamBuilderContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<EventTeam> EventTeams { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string modelsNamespace = "TeamBuilder.Models";
            string configurationsNamespace = "TeamBuilder.ModelConfiguration";

            Assembly modelsAssembly = Assembly.Load(modelsNamespace);
            Type[] modelTypes = modelsAssembly
                                    .GetTypes()
                                    .ToArray();

            Assembly modelsConfigurationAssembly = Assembly.Load(configurationsNamespace);
            Type[] configurationTypes = modelsConfigurationAssembly
                                            .GetTypes()
                                            .ToArray();

            foreach (Type modelType in modelTypes)
            {
                Type configurationType = configurationTypes.SingleOrDefault(n => n.Name == modelType.Name + "Configuration");

                if (configurationType != null)
                {
                    MethodInfo configureMethod = this.GetType().GetMethod("Configure", BindingFlags.Instance | BindingFlags.NonPublic);
                    MethodInfo genericConfig = configureMethod.MakeGenericMethod(modelType, configurationType);

                    genericConfig.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void Configure<TModel, TModelConfig>(ModelBuilder modelBuilder)
            where TModel : class
            where TModelConfig : IEntityTypeConfiguration<TModel>
        {
            IEntityTypeConfiguration<TModel> configuration = Activator.CreateInstance<TModelConfig>();
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}