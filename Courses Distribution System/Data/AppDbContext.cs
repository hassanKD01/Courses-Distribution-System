using System.Linq;
using Courses_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ProfessorsDepartments> ProfessorsDepartments { get; set; }
        public DbSet<ProfessorHours> ProfessorHours { get; set; }
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>().HasKey(p => p.Id);
            builder.Entity<Professor>().Property(p=>p.Id).ValueGeneratedNever();
            builder.Entity<ProfessorsDepartments>().HasKey(table => new { table.DepartmentName, table.ProfessorId });
            builder.Entity<ProfessorHours>().HasKey(table => new { table.AcademicYear, table.ProfessorId });

            builder.Entity<Course>().HasOne("Department").WithMany("Courses")
                .HasForeignKey("DepartmentName").OnDelete(DeleteBehavior.Cascade).IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
