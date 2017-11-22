namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(pk => pk.StudentId);

            builder.HasMany(hs => hs.HomeworkSubmissions)
                   .WithOne(s => s.Student)
                   .HasForeignKey(fk => fk.StudentId);

            builder.HasMany(sc => sc.CourseEnrollments)
                   .WithOne(s => s.Student);

            builder.Property(p => p.Name)
                   .IsUnicode(true)
                   .HasMaxLength(100);

            builder.Property(p => p.PhoneNumber)
                   .IsRequired(false)
                   .HasColumnType("char(10)");

            builder.Property(p => p.Birthday)
                   .IsRequired(false);
        }
    }
}
