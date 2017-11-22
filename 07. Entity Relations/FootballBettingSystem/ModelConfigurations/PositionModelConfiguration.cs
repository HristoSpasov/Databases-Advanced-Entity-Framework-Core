namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PositionModelConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> entity)
        {
            entity.ToTable("Positions");

            entity.HasKey(pk => pk.PositionId);

            entity.HasMany(p => p.Players)
                  .WithOne(pos => pos.Position)
                  .HasForeignKey(fk => fk.PositionId);
        }
    }
}
