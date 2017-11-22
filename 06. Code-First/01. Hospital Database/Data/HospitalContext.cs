namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientMedicament> PatientMedicament { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Patient configuration
            builder.Entity<Patient>()
                .HasKey(k => k.PatientId);

            builder.Entity<Patient>()
                .HasMany(v => v.Visitations)
                .WithOne(v => v.Patient)
                .HasForeignKey(fk => fk.PatientId);

            builder.Entity<Patient>()
                .HasMany(d => d.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(fk => fk.PatientId);

            builder.Entity<Patient>()
                .HasMany(pm => pm.Prescriptions)
                .WithOne(p => p.Patient)
                .HasForeignKey(fk => fk.PatientId);

            builder.Entity<Patient>()
                .Property(p => p.FirstName)
                .HasColumnType("nvarchar(50)");

            builder.Entity<Patient>()
                .Property(p => p.LastName)
                .HasColumnType("nvarchar(50)");

            builder.Entity<Patient>()
                .Property(p => p.Address)
                .HasColumnType("nvarchar(250)");

            builder.Entity<Patient>()
                .Property(p => p.Email)
                .HasColumnType("varchar(80)");

            // Visitation configuration
            builder.Entity<Visitation>()
                .HasKey(k => k.VisitationId);

            builder.Entity<Visitation>()
                .HasOne(p => p.Patient)
                .WithMany(v => v.Visitations);

            builder.Entity<Visitation>()
                .HasOne(d => d.Doctor)
                .WithMany(v => v.Visitations);

            builder.Entity<Visitation>()
                .Property(p => p.Comments)
                .HasColumnType("nvarchar(250)");

            // Diagnose configuration
            builder.Entity<Diagnose>()
                .HasKey(k => k.DiagnoseId);

            builder.Entity<Diagnose>()
                .HasOne(p => p.Patient)
                .WithMany(d => d.Diagnoses);

            builder.Entity<Diagnose>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(50)");

            builder.Entity<Diagnose>()
                .Property(p => p.Comments)
                .HasColumnType("nvarchar(250)");

            // Medicament configuration
            builder.Entity<Medicament>()
                .HasKey(k => k.MedicamentId);

            builder.Entity<Medicament>()
                .HasMany(pr => pr.Prescriptions)
                .WithOne(m => m.Medicament)
                .HasForeignKey(fk => fk.MedicamentId);

            builder.Entity<Medicament>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(50)");

            // Doctor config
            builder.Entity<Doctor>()
                .HasKey(k => k.DoctorId);

            builder.Entity<Doctor>()
                .HasMany(v => v.Visitations)
                .WithOne(d => d.Doctor)
                .HasForeignKey(d => d.DoctorId);

            builder.Entity<Doctor>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(100)");

            builder.Entity<Doctor>()
                .Property(p => p.Specialty)
                .HasColumnType("nvarchar(100)");

            // PatientMedicament config
            builder.Entity<PatientMedicament>()
                .HasKey(k => new { k.MedicamentId, k.PatientId });
        }
    }
}