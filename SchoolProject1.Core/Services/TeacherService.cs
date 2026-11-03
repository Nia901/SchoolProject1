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
        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await context.Teachers.Include(s => s.StudentTeacher)
                .ThenInclude(st => st.Student).ToListAsync();
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
                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync();
            }
        }
    }
}
