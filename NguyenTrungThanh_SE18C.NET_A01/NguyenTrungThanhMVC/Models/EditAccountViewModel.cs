using System.ComponentModel.DataAnnotations;

namespace NguyenTrungThanhMVC.Models
{
    public class EditAccountViewModel
    {
        [Required]
        public short AccountId { get; set; }

        [Required(ErrorMessage = "Account name is required.")]
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Range(1, 2, ErrorMessage = "Role must be Staff (1) or Lecturer (2).")]
        public short AccountRole { get; set; }
    }
}