using Data.Entities;
using Data;
using SchoolProject1.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject1.Core.Services
{
    public class TeacherService:ITeacherService
    {
        private SchoolDbContext context;
        public TeacherService(SchoolDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Teacher>> GetAllTeachersAsync(
    string? name = null,
    string? subject = null,
    string? sortOrder = null)
        {
            var query = context.Teachers
                .Include(d => d.School)
                .Include(d => d.StudentTeacher)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d =>
                    d.FirstName.Contains(name) ||
                    d.LastName.Contains(name));
            }

            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(d => d.Subject.Contains(subject));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(d => d.FirstName)
                        .ThenByDescending(d => d.LastName);
                    break;

                case "name_asc":
                    query = query.OrderBy(d => d.FirstName)
                        .ThenBy(d => d.LastName);
                    break;

                case "subject":
                    query = query.OrderBy(d => d.Subject)
                        .ThenBy(d => d.FirstName);
                    break;

                default:
                    query = query.OrderBy(d => d.FirstName)
                        .ThenBy(d => d.LastName);
                    break;
            }

            return await query.ToListAsync();
        }
        public async Task<Teacher>? GetTeacherByIdAsync(int id)
        {
            return await context.Teachers.FindAsync(id);
        }
        public async Task AddTeacherAsync(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            await context.SaveChangesAsync();
        }
        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteTeacherAsync(int id)
        {
            Teacher teacher = await context.Teachers.FindAsync(id);

            if (teacher != null)
            {
                int schoolId = teacher.SchoolId;

                var school = await context.Schools
                    .FirstOrDefaultAsync(x => x.Id == schoolId);

                var studentTeachers = context.StudentsTeachers.Include(s=>s.Student)
                    .Where(x => x.TeacherId == teacher.Id)
                    .ToList();


                if (school != null)
                {
                    school.StudentsCount -= studentTeachers.Count;
                }
                context.Students.RemoveRange(studentTeachers.Select(t => t.Student));
                context.StudentsTeachers.RemoveRange(studentTeachers);
                


                context.Teachers.Remove(teacher);

                await context.SaveChangesAsync();
            }
        }
    }
}
