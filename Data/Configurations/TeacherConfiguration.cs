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
    public class TeacherConfiguration:IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Age).IsRequired();
            builder.Property(t => t.YearsOfExperience).IsRequired();
            builder.Property(t => t.Address).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.PhoneNumber).IsRequired();
            builder.Property(t => t.Subject).IsRequired().HasMaxLength(50);
            builder.HasOne(t => t.School).WithMany(t => t.Teachers).HasForeignKey(t => t.SchoolId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "Teodora",
                    LastName = "Koleva",
                    Age = 30,
                    YearsOfExperience=3,
                    Address = "Kazanlak",
                    Email = "teodorakoleva1@abv.bg",
                    PhoneNumber = "0888 123 876",
                    Subject = "chemistry",
                    SchoolId = 1
                },
              new Teacher
              {
                  Id = 2,
                  FirstName = "Svetlana",
                  LastName = "Nenkova",
                  Age = 50,
                  YearsOfExperience = 10,
                  Address = "Kazanlak",
                  Email = "svetlananenkova1@abv.bg",
                  PhoneNumber = "0888 123 876",
                  Subject = "maths",
                  SchoolId = 2
              },
                new Teacher
                {
                    Id = 3,
                    FirstName = "Kamen",
                    LastName = "Kolev",
                    Age = 35,
                    YearsOfExperience = 12,
                    Address = "Sofia",
                    Email = "kamenkolev1@abv.bg",
                    PhoneNumber = "0888 123 333",
                    Subject = "physics",
                    SchoolId = 3
                }
            );
        }
    }
}
