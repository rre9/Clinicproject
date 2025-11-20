using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClinicProject;   // مهم علشان enum AppRoles
using ClinicProject.Models; // علشان AppUser

namespace ClinicProject.Models
{
    public class ClinicContextcs : IdentityDbContext<AppUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public ClinicContextcs(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rename Identity Tables
            modelBuilder.Entity<AppUser>(e => e.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));

            // Seed Default Roles
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Id = "ad90e18a-ab7f-4bb0-9f73-5778de3b4a1f", Name = AppRoles.APP_ADMIN.ToString(), NormalizedName = AppRoles.APP_ADMIN.ToString(), ConcurrencyStamp = "ad90e18a-ab7f-4bb0-9f73-5778de3b4a1f" },
                    new IdentityRole { Id = "ad90e18a-ab7f-4bb0-9f74-5778de3b4a1f", Name = AppRoles.DOCTOR.ToString(), NormalizedName = AppRoles.DOCTOR.ToString(), ConcurrencyStamp = "ad90e18a-ab7f-4bb0-9f74-5778de3b4a1f" },
                    new IdentityRole { Id = "ad90e18a-ab7f-4bb0-9f75-5778de3b4a1f", Name = AppRoles.RECEPTIONIST.ToString(), NormalizedName = AppRoles.RECEPTIONIST.ToString(), ConcurrencyStamp = "ad90e18a-ab7f-4bb0-9f75-5778de3b4a1f" }
                );

            modelBuilder.Entity<Doctor>()
                .Property(d => d.LastName)
                .HasDefaultValue("Alfulani");

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Speciality)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialityNum)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Speciality>()
                .HasKey(s => s.Code);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Speciality>()
                .HasData(
                    new Speciality { Code = 1, Name = "Orthopidcs" },
                    new Speciality { Code = 2, Name = "Dental" },
                    new Speciality { Code = 3, Name = "Pediatric" },
                    new Speciality { Code = 5, Name = "General" }
                );
        }
    }
}
