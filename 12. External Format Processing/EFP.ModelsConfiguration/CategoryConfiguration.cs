namespace EFP.ModelsConfiguration
{
    using EFP.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Name)
                .IsRequired(true)
                .IsUnicode(true);

            builder.HasMany(cp => cp.CategoryProducts)
                .WithOne(c => c.Category);
        }
    }
}