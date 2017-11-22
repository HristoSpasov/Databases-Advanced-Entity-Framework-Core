namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class TeamModelConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> entity)
        {
            entity.ToTable("Teams");

            entity.HasKey(pk => pk.TeamId);

            entity.HasMany(p => p.Players)
                  .WithOne(t => t.Team)
                  .HasForeignKey(fk => fk.TeamId);

            entity.HasMany(hg => hg.HomeGames)
                  .WithOne(g => g.HomeTeam)
                  .HasForeignKey(fk => fk.HomeTeamId);

            entity.HasMany(at => at.AwayGames)
                  .WithOne(g => g.AwayTeam)
                  .HasForeignKey(fk => fk.AwayTeamId);

            entity.HasOne(pc => pc.PrimaryKitColor)
                  .WithMany(c => c.PrimaryKitTeams)
                  .HasForeignKey(fk => fk.PrimaryKitColorId);

            entity.HasOne(sc => sc.SecondaryKitColor)
                  .WithMany(c => c.SecondaryKitTeams)
                  .HasForeignKey(fk => fk.SecondaryKitColorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Town)
                  .WithMany(te => te.Teams)
                  .HasForeignKey(fk => fk.TownId);
        }
    }
}
