using System.ComponentModel.DataAnnotations;

namespace EducationApp.BusinessLogicLayer.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password length from 6 to 50 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm")]
        public string PasswordConfirm { get; set; }
    }
}
