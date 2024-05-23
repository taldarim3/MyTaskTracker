using System.ComponentModel.DataAnnotations;

namespace MyTaskTracker.ViewModels;

public class AddOrganizationViewModel
{
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public string Name { get; set; }

    [Display(Name = "Описание проекта")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public string? Description { get; set; } 
    
}