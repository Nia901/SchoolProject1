using System.ComponentModel.DataAnnotations;

namespace SchoolProject1.ViewModels.School
{
    public class SchoolDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolAddress { get; set; }
        public int StudentsCount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
