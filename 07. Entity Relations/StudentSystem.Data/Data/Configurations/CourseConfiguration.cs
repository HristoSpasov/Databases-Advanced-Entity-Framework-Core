namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(pk => pk.CourseId);

            builder.HasMany(r => r.Resources)
                   .WithOne(c => c.Course)
                   .HasForeignKey(fk => fk.CourseId);

            builder.HasMany(hs => hs.HomeworkSubmissions)
                   .WithOne(c => c.Course)
                   .HasForeignKey(fk => fk.CourseId);

            builder.HasMany(sc => sc.StudentsEnrolled)
                   .WithOne(c => c.Course);

            builder.Property(p => p.Name)
                   .HasMaxLength(80)
                   .IsUnicode(true);

            builder.Property(p => p.Description)
                   .IsUnicode(true)
                   .IsRequired(false);
        }
    }
}
