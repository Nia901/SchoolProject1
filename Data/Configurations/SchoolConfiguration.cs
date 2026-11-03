using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class SchoolConfiguration:IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasData(
                new School
                {
                    Id = 1,
                    Name="OU \"Georgi Kirkov\"",
                    SchoolAddress = "Kazanlak, Bulgaria",
                    StudentsCount = 0,
                    Email="georgikirkovschool1@abv.bg",
                    PhoneNumber = "0888 123 456",
                    RoomsCount = 10,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                },
                new School
                {
                    Id = 2,
                    Name = "PPMG \"Nikola Obreshkov\"",
                    SchoolAddress = "Kazanlak, Bulgaria",
                    StudentsCount = 0,
                    Email = "nikolaobreshkovschool1@abv.bg",
                    PhoneNumber = "0888 123 459",
                    RoomsCount = 15,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                },
                 new School
                 {
                     Id = 3,
                     Name = "SU \"Kiril i Metodii\"",
                     SchoolAddress = "Sofia, Bulgaria",
                     StudentsCount = 0,
                     Email = "kirilimetodiischool1@abv.bg",
                     PhoneNumber = "0888 123 459",
                     RoomsCount = 8,
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 }
            );
        }
    }
}
