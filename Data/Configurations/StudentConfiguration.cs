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
    public class StudentConfiguration:IEntityTypeConfiguration<Student>
    {
        //problema s uchitelite v nachaloto i problema s adresite
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                new Student
                {
                    Id = 1,
                    FirstName= "Ivan",
                    LastName = "Ivanov",
                    Age=18,
                    Address = "Kazanlak, Bulgaria",
                    Email="ivanivanov1@abv.bg",
                    PhoneNumber = "0888 123 456"
                },
               new Student
               {
                   Id = 2,
                   FirstName = "Petar",
                   LastName = "Petrov",
                   Age = 15,
                   Address = "Kazanlak, Bulgaria",
                   Email = "petarpetrov1@abv.bg",
                   PhoneNumber = "0888 123 450"
               },
                 new Student
                 {
                     Id = 3,
                     FirstName = "Dariya",
                     LastName = "Ivanova",
                     Age = 14,
                     Address = "Sofia, Bulgaria",
                     Email = "dariyaivanova1@abv.bg",
                     PhoneNumber = "0888 123 468"
                 }
            );
        }
    }
}
