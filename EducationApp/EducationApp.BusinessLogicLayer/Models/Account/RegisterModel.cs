using System.ComponentModel.DataAnnotations;

namespace EducationApp.BusinessLogicLayer.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Укажите Email")]
        [EmailAddress(ErrorMessage = "Не корректный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля от 6 до 50 сиволов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите подтверждающий пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
