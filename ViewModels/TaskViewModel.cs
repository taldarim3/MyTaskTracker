namespace MyTaskTracker.ViewModels;
using Task = MyTaskTracker.Models.Task;

public class TaskViewModel
{
    public IEnumerable<Task> Tasks { get; set; }
}