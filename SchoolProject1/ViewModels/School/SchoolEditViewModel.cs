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
        [Required(ErrorMessage = "School email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Rooms count is required.")]
        [Range(1,100,ErrorMessage = "Invalid rooms count. (Rooms count must be less than 100 and more than 1.)")]
        public int RoomsCount { get; set; }
    }
}
