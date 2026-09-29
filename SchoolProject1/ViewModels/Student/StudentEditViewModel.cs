using System.ComponentModel.DataAnnotations;

namespace SchoolProject1.ViewModels.Student
{
    public class StudentEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "String length must be less than 50.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "String length must be less than 50.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 18, ErrorMessage = "Age must be between 1 and 18.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "String length must be less than 100.")]
        public string Address { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        public int SchoolId { get; set; }
    }
}
