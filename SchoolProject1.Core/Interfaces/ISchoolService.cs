using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject1.Core.Interfaces
{
    public interface ISchoolService
    {
        public Task<List<School>> GetAllSchoolsAsync(string? search = null, int? minStudents = null, string? sortOrder = null);
        public Task<School>? GetSchoolByIdAsync(int id);
        public Task AddSchoolAsync(School school);
        public Task UpdateSchoolAsync(School school);
        public Task DeleteSchoolAsync(int id);
    }
}
