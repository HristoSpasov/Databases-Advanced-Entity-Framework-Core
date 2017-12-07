namespace TeamBuilder.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(250);

            builder.Property(p => p.StartDate)
                .IsRequired(true);

            builder.Property(p => p.EndDate)
                .IsRequired(true);

            builder.HasOne(u => u.Creator)
                .WithMany(e => e.Events)
                .HasForeignKey(fk => fk.CreatorId);
        }
    }
}