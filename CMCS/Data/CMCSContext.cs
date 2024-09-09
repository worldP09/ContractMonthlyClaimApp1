using Microsoft.EntityFrameworkCore;
using CMCS.Models;

namespace CMCS.Data
{
    public class CMCSContext : DbContext
    {
        public CMCSContext(DbContextOptions<CMCSContext> options)
            : base(options)
        {
        }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<AcademicManager> AcademicManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Claim>()
                .Property(c => c.HoursWorked)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<Claim>()
                .Property(c => c.TotalClaimAmount)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<Lecturer>()
                .Property(l => l.HourlyRate)
                .HasColumnType("decimal(18,2)");
        }

    }
}
