using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject1.Core.Interfaces
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentsAsync();
        public Task<Student>? GetStudentByIdAsync(int id);
        public Task AddStudentAsync(Student student,int schoolId);
        public Task UpdateStudentAsync(int teacherId,int oldTeach);
        public Task DeleteStudentAsync(int id,int? schoolId=null);
    }
}
