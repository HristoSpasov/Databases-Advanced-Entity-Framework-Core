namespace Employees.ModelsConfig
{
    using Employees.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.FirstName)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(p => p.LastName)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(p => p.Salary)
                .IsRequired(true)
                .IsUnicode(true);

            builder.Property(p => p.Birthday)
                .IsRequired(false);

            builder.Property(p => p.Address)
                .IsRequired(false);

            builder.Property(p => p.ManagerId)
                .IsRequired(false);

            builder.HasOne(m => m.Manager)
                .WithMany(e => e.Employees)
                .HasForeignKey(m => m.Id == m.ManagerId);
        }
    }
}