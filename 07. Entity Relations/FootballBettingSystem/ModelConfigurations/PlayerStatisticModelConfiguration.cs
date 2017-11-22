namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerStatisticModelConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> entity)
        {
            entity.ToTable("PlayerStatistics");

            entity.HasKey(pk => new { pk.PlayerId, pk.GameId });

            entity.HasOne(g => g.Game)
                  .WithMany(ps => ps.PlayerStatistics);

            entity.HasOne(p => p.Player)
                  .WithMany(ps => ps.PlayerStatistics);
        }
    }
}
