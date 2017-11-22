namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class ColorModelConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> entity)
        {
            entity.ToTable("Colors");

            entity.HasKey(pk => pk.ColorId);

            entity.HasMany(pkt => pkt.PrimaryKitTeams)
                  .WithOne(ptc => ptc.PrimaryKitColor)
                  .HasForeignKey(fk => fk.PrimaryKitColorId);

            entity.HasMany(skt => skt.SecondaryKitTeams)
                  .WithOne(stc => stc.SecondaryKitColor)
                  .HasForeignKey(fk => fk.SecondaryKitColorId);
        }
    }
}
