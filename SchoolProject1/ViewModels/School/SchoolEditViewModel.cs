using System.ComponentModel.DataAnnotations;

namespace SchoolProject1.ViewModels.School
{
    public class SchoolEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "String length must be less than 100.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "School address is required.")]
        [StringLength(100, ErrorMessage = "String length must be less than 100.")]
        public string SchoolAddress { get; set; }
    }
}
