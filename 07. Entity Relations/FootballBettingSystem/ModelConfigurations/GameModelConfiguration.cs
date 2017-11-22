namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class GameModelConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.ToTable("Games");

            entity.HasKey(pk => pk.GameId);

            entity.HasMany(b => b.Bets)
                  .WithOne(g => g.Game)
                  .HasForeignKey(fk => fk.GameId);

            entity.HasMany(ps => ps.PlayerStatistics)
                  .WithOne(g => g.Game)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ht => ht.HomeTeam)
                  .WithMany(g => g.HomeGames)
                  .HasForeignKey(fk => fk.HomeTeamId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(at => at.AwayTeam)
                  .WithMany(g => g.AwayGames)
                  .HasForeignKey(fk => fk.AwayTeamId);
        }
    }
}
