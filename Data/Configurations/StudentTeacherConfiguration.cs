using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class StudentTeacherConfiguration : IEntityTypeConfiguration<StudentTeacher>
    {
        public void Configure(EntityTypeBuilder<StudentTeacher> builder)
        {
            builder.HasKey(dp => new { dp.StudentId, dp.TeacherId });
            builder.HasOne(dp => dp.Teacher)
                .WithMany(d => d.StudentTeacher)
                .HasForeignKey(dp => dp.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dp => dp.Student)
                .WithMany(d => d.StudentTeacher)
                .HasForeignKey(dp => dp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                 new StudentTeacher
                 {
                     StudentId = 1,
                     TeacherId = 1
                 },
                new StudentTeacher
                {
                    StudentId = 2,
                    TeacherId = 3
                },
                  new StudentTeacher
                  {
                      StudentId = 3,
                      TeacherId = 3
                  }
             );
        }
    }
}
