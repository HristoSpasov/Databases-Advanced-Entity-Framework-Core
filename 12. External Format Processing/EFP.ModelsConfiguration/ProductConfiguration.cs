namespace EFP.ModelsConfiguration
{
    using EFP.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasIndex(pk => pk.Id);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(p => p.Price)
                .IsRequired(true);

            builder.Property(p => p.BuyerId)
                .IsRequired(false);

            builder.Property(p => p.SellerId)
                .IsRequired(true);

            builder.HasOne(u => u.Seller)
                .WithMany(u => u.ProductsSold)
                .HasForeignKey(s => s.SellerId);

            builder.HasOne(u => u.Buyer)
                .WithMany(u => u.ProductsBought)
                .HasForeignKey(fk => fk.BuyerId);

            builder.HasMany(cp => cp.CategoryProducts)
                .WithOne(p => p.Product);
        }
    }
}