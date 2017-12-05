namespace EFP.ModelsConfiguration
{
    using EFP.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.ToTable("CategoryProducts");

            builder.HasKey(pk => new { pk.ProductId, pk.CategoryId });
        }
    }
}