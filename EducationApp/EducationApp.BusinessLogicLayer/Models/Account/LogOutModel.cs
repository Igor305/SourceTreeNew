using System.ComponentModel.DataAnnotations;

namespace EducationApp.BusinessLogicLayer.Models.Account
{
    public class LogOutModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
