namespace TeamBuilder.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(pk => pk.Id);

            builder.HasIndex(p => p.Username)
                .IsUnique(true);

            builder.Property(p => p.Username)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(p => p.FirstName)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(25);

            builder.Property(p => p.LastName)
               .IsRequired(false)
               .IsUnicode(true)
               .HasMaxLength(25);

            builder.Property(p => p.Password)
              .IsRequired(true)
              .IsUnicode(false);

            builder.Property(p => p.Gender)
                .IsRequired(true);

            builder.Property(p => p.Age)
                .IsRequired(true);

            builder.HasMany(i => i.Invitations)
                .WithOne(u => u.InvitedUser)
                .HasForeignKey(fk => fk.InvitedUserId);

            builder.HasMany(e => e.Events)
                .WithOne(u => u.Creator)
                .HasForeignKey(fk => fk.CreatorId);

            builder.HasMany(t => t.Teams)
                .WithOne(u => u.Creator)
                .HasForeignKey(fk => fk.CreatorId);

            builder.HasMany(ut => ut.UserTeams)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}