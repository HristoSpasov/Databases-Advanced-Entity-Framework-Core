namespace P03_FootballBetting.Data
{
    using System;
    using System.Reflection;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using P03_FootballBetting.ModelConfigurations;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=M6800\SQLEXPRESS;Database=FootballBettingDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string modelConfigurationEnd = "ModelConfiguration";

            Type[] modelTypes = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(t => t.Namespace == "P03_FootballBetting.Data.Models")
                                        .ToArray();

            foreach (Type modelType in modelTypes)
            {
                Type configurationType = Assembly.GetExecutingAssembly()
                                                 .GetTypes()
                                                 .First(t => t.Name == modelType.Name + modelConfigurationEnd);

                MethodInfo configureMethodInfo = typeof(FootballBettingContext).GetMethod("Configure", BindingFlags.Instance | BindingFlags.NonPublic);
                MethodInfo genericConfigure = configureMethodInfo.MakeGenericMethod(modelType, configurationType);
                genericConfigure.Invoke(this, new object[] { modelBuilder });
            }
        }

        private void Configure<TClass, TConfig>(ModelBuilder modelBuilder)
            where TClass : class
            where TConfig : IEntityTypeConfiguration<TClass>
        {
            IEntityTypeConfiguration<TClass> configuration = Activator.CreateInstance<TConfig>();
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}
