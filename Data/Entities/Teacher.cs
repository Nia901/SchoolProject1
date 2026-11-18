using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required,StringLength(50)]
        public string LastName { get; set; }
        [Required, Range(1, 100)]
        public int Age { get; set; }
        [Required,Range(0,100)]
        public int YearsOfExperience { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
        [Required, StringLength(100)]
        public string Address { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public ICollection<StudentTeacher> StudentTeacher { get; set; } = new List<StudentTeacher>();
    }
}
