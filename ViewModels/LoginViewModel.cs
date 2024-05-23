using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyTaskTracker.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
