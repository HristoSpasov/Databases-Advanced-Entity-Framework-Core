namespace TeamBuilder.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("Invitations");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(u => u.InvitedUser)
                .WithMany(i => i.Invitations)
                .HasForeignKey(fk => fk.InvitedUserId);

            builder.HasOne(t => t.Team)
                .WithMany(i => i.Invitations)
                .HasForeignKey(fk => fk.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}