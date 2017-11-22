namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerModelConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> entity)
        {
            entity.ToTable("Players");

            entity.HasKey(pk => pk.PlayerId);

            entity.HasMany(ps => ps.PlayerStatistics)
                  .WithOne(p => p.Player);

            entity.HasOne(po => po.Position)
                  .WithMany(pl => pl.Players)
                  .HasForeignKey(fk => fk.PositionId);

            entity.HasOne(t => t.Team)
                  .WithMany(pl => pl.Players)
                  .HasForeignKey(fk => fk.TeamId);
        }
    }
}
