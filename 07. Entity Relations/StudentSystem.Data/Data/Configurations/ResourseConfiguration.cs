namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class ResourseConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources");

            builder.HasKey(pk => pk.ResourceId);

            builder.HasOne(c => c.Course)
                   .WithMany(r => r.Resources)
                   .HasForeignKey(fk => fk.CourseId);

            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsUnicode(true);

            builder.Property(p => p.Url)
                   .IsUnicode(false);
        }
    }
}
