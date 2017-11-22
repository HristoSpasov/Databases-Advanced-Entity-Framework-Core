namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class CountryModelConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Countries");

            entity.HasKey(pk => pk.CountryId);

            entity.HasMany(t => t.Towns)
                  .WithOne(c => c.Country)
                  .HasForeignKey(fk => fk.TownId);
        }
    }
}
