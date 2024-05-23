using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyTaskTracker.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        public string Surname { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email обязаетелен для заполнения")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Требуется установить пароля")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Подтвердите пароль")]
        [Required(ErrorMessage = "Требуется подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
