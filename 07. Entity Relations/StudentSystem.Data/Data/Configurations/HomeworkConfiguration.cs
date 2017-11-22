namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.ToTable("HomeworkSubmissions");

            builder.HasKey(pk => pk.HomeworkId);

            builder.HasOne(s => s.Student)
                   .WithMany(hs => hs.HomeworkSubmissions)
                   .HasForeignKey(fk => fk.StudentId);

            builder.HasOne(c => c.Course)
                   .WithMany(hs => hs.HomeworkSubmissions)
                   .HasForeignKey(fk => fk.CourseId);

            builder.Property(p => p.Content)
                   .IsUnicode(false);
        }
    }
}
