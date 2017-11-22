namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class TownModelConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> entity)
        {
            entity.ToTable("Towns");

            entity.HasKey(pk => pk.TownId);

            entity.HasMany(t => t.Teams)
                  .WithOne(to => to.Town)
                  .HasForeignKey(fk => fk.TownId);

            entity.HasOne(c => c.Country)
                  .WithMany(t => t.Towns)
                  .HasForeignKey(fk => fk.TownId);
        }
    }
}
