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
            builder.HasData(
                new Teacher
                {
                    Id = 1,
                    FirstName = "Teodora",
                    LastName = "Koleva",
                    Age = 30,
                    YearsOfExperience=3,
                    Address = "Kazanlak, Bulgaria",
                    Email = "teodorakoleva1@abv.bg",
                    PhoneNumber = "0888 123 876",
                    SchoolId = 1
                },
              new Teacher
              {
                  Id = 2,
                  FirstName = "Svetlana",
                  LastName = "Nenkova",
                  Age = 50,
                  YearsOfExperience = 10,
                  Address = "Kazanlak, Bulgaria",
                  Email = "svetlananenkova1@abv.bg",
                  PhoneNumber = "0888 123 876",
                  SchoolId = 2
              },
                new Teacher
                {
                    Id = 3,
                    FirstName = "Kamen",
                    LastName = "Kolev",
                    Age = 35,
                    YearsOfExperience = 12,
                    Address = "Sofia, Bulgaria",
                    Email = "kamenkolev1@abv.bg",
                    PhoneNumber = "0888 123 333",
                    SchoolId = 3
                }
            );
        }
    }
}
