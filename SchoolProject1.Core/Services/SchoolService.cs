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
        public async Task<List<School>> GetAllSchoolsAsync(string? search = null, int? minStudents = null, string? sortOrder = null)
        {
            var query = context.Schools.Where(h => !h.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(h => h.Name.Contains(search));
            }

            if (minStudents.HasValue)
            {
                query = query.Where(h => h.StudentsCount >= minStudents.Value);
            }
            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(h => h.Name);
                    break;
                case "students_desc":
                    query = query.OrderByDescending(h => h.StudentsCount);
                    break;
                case "students_asc":
                    query = query.OrderBy(h => h.StudentsCount);
                    break;
                default:
                    query = query.OrderBy(h => h.Name);
                    break;
            }
            return await query.ToListAsync();
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
            if (school == null) return;
            bool hasTeachers = await context.Teachers.AnyAsync(t => t.SchoolId == id);
            if (hasTeachers)
            {
                throw new InvalidOperationException("Uchilishteto ne moje da bude iztrito, zashtoto v nego ima uchiteli");
            }
            school.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }
}
