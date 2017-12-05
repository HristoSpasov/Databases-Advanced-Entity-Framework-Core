namespace EFP.ModelsConfiguration
{
    using EFP.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Age)
                .IsRequired(false);

            builder.Property(p => p.FirstName)
               .IsRequired(false)
               .IsUnicode(true);

            builder.Property(p => p.LastName)
               .IsRequired(true)
               .IsUnicode(true);

            builder.HasMany(p => p.ProductsBought)
                .WithOne(u => u.Buyer)
                .HasForeignKey(fk => fk.BuyerId);

            builder.HasMany(p => p.ProductsSold)
                .WithOne(u => u.Seller)
                .HasForeignKey(fk => fk.SellerId);
        }
    }
}