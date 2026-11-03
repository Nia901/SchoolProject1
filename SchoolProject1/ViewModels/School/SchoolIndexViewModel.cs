using System.ComponentModel.DataAnnotations;

namespace SchoolProject1.ViewModels.School
{
    public class SchoolIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolAddress { get; set; }
        public int StudentsCount { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoomsCount { get; set; }
    }
}
