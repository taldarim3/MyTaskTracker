namespace MyTaskTracker.ViewModels;
using System.ComponentModel.DataAnnotations;

public class AddTaskViewModel
{
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public string Name { get; set; }

    [Display(Name = "Описание задачи")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public string? Description { get; set; } 
    
    [Display(Name = "Сложность")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public int Difficulty { get; set; }
    
    public int TaskStatus { get; set; }

    [Display(Name = "Время на выполнение")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public double TimeEstimation { get; set; }
    
    [Display(Name = "Пользователь")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public string userEmail { get; set; }

    [Display(Name = "Проект")]
    [Required(ErrorMessage = "Обязательно для заполнения")]
    public int OrganizationId { get; set; }
}