namespace TeamBuilder.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.HasIndex(n => n.Name).IsUnique(true);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(32);

            builder.Property(p => p.Acronym)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasMany(et => et.EventTeams)
                .WithOne(t => t.Team)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}