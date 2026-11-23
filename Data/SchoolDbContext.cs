using Data.Configurations;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentTeacher> StudentsTeachers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //bez fluentapi
            //modelBuilder.Entity<StudentTeacher>()
            //    .HasKey(dp => new { dp.StudentId, dp.TeacherId });

            //modelBuilder.Entity<StudentTeacher>()
            //    .HasOne(dp => dp.Student)
            //    .WithMany(d => d.StudentTeacher)
            //    .HasForeignKey(dp => dp.StudentId)
            //    .OnDelete(DeleteBehavior.Cascade);//za da se iztriqt zapisite v i dvete tablici

            //modelBuilder.Entity<StudentTeacher>()
            //    .HasOne(dp => dp.Teacher)
            //    .WithMany(p => p.StudentTeacher)
            //    .HasForeignKey(dp => dp.TeacherId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTeacherConfiguration());
        }

    }
}
