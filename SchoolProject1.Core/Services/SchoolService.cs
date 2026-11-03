using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolProject1.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject1.Core.Services
{
    public class SchoolService:ISchoolService
    {
        private SchoolDbContext context;
        public SchoolService(SchoolDbContext context)
        {
            this.context = context;
        }
        public async Task<List<School>> GetAllSchoolsAsync()
        {
            return await context.Schools.Where(s=>!s.IsDeleted).ToListAsync();
        }
        public async Task<School>? GetSchoolByIdAsync(int id)
        {
            return await context.Schools.FindAsync(id);
        }
        public async Task AddSchoolAsync(School school)
        {
            context.Schools.Add(school);
            await context.SaveChangesAsync();
        }
        public async Task UpdateSchoolAsync(School school)
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteSchoolAsync(int id)
        {
            School school = await context.Schools.FindAsync(id);
            if (school != null)
            {
                school.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
