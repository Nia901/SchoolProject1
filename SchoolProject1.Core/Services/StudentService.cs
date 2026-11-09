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
    public class StudentService:IStudentService
    {
        private SchoolDbContext context;
        public StudentService(SchoolDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await context.Students.Include(s => s.StudentTeacher)
                .ThenInclude(st => st.Teacher).ToListAsync();
        }
        public async Task<Student>? GetStudentByIdAsync(int id)
        {
            return await context.Students
    .Include(s => s.StudentTeacher)
    .ThenInclude(t=>t.Teacher)
    .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task AddStudentAsync(Student student,int schoolId)
        {
            School school = await context.Schools.FindAsync(schoolId);
            if (school != null)
            {
                school.StudentsCount++;
                context.Students.Add(student);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateStudentAsync(Student student)
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteStudentAsync(int id,int schoolId)
        {
            School school = await context.Schools.FindAsync(schoolId);
            Student student = await context.Students.FindAsync(id);
            if (student != null&&school!=null)
            {
                school.StudentsCount--;
                context.Students.Remove(student);
                await context.SaveChangesAsync();
            }
        }
    }
}
