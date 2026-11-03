using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject1.Core.Interfaces
{
    public interface ITeacherService
    {
        public Task<List<Teacher>> GetAllTeachersAsync();
        public Task<Teacher>? GetTeacherByIdAsync(int id);
        public Task AddTeacherAsync(Teacher teacher);
        public Task UpdateTeacherAsync(Teacher teacher);
        public Task DeleteTeacherAsync(int id);
    }
}
