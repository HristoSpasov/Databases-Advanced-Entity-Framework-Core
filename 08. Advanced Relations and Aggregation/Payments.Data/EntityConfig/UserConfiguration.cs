namespace Payments.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Payments.Entities;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(pk => pk.UserId);

            builder.HasMany(pm => pm.PaymentMethods)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId);

            builder.Property(p => p.FirstName)
                .IsRequired(true)
                .HasColumnType("nvarchar(50)");

            builder.Property(p => p.LastName)
                .IsRequired(true)
                .HasColumnType("nvarchar(50)");

            builder.Property(p => p.Email)
                .IsRequired(true)
                .HasColumnType("varchar(80)");

            builder.Property(p => p.Password)
                .IsRequired(true)
                .HasColumnType("varchar(25)");
        }
    }
}