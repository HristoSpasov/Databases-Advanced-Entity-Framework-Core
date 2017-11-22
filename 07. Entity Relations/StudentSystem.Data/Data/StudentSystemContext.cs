namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Configurations;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder contextBuilder)
        {
            base.OnConfiguring(contextBuilder);
            
            if (!contextBuilder.IsConfigured)
            {
                contextBuilder.UseSqlServer(@"Server=M6800\SQLEXPRESS;Database=StudentSystem;Integrated Security=True");
            }
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StudentConfiguration());

            builder.ApplyConfiguration(new CourseConfiguration());

            builder.ApplyConfiguration(new HomeworkConfiguration());

            builder.ApplyConfiguration(new ResourseConfiguration());

            builder.ApplyConfiguration(new StudentCourseConfiguration());
        }
    }
}
