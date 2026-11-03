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
