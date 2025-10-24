using System.ComponentModel.DataAnnotations;

namespace NguyenTrungThanhMVC.Models
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Account name is required.")]
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("AccountPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Range(1, 2, ErrorMessage = "Role must be Staff (1) or Lecturer (2).")]
        public short AccountRole { get; set; }
    }
}