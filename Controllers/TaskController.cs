using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTaskTracker.Interfaces;
using MyTaskTracker.Models.Common.Enums;
using MyTaskTracker.ViewModels;
using TaskStatus = MyTaskTracker.Models.Common.Enums.TaskStatus;

namespace MyTaskTracker.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    [Route("Tasks/{email}")]
    public async Task<IActionResult> TasksByUser(string email)
    {

        var tasks = await _taskRepository.GetAllByUser(email);

        var taskViewModel = new TaskViewModel()
        {
            Tasks = tasks
        };

        return View(taskViewModel);
    }

    public async Task<IActionResult> SetNextStatus(int taskId, string email, bool step)
    {

        var flag = _taskRepository.SetNextStatus(taskId, step);

        return RedirectToAction("TasksByUser", new { email });
    }

    
    [HttpGet]
    public IActionResult AddTask()
    {
        var response = new AddTaskViewModel();
        return View(response);
    } 
    
    [HttpPost]
    public async Task<IActionResult> AddTask(AddTaskViewModel addTaskViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(addTaskViewModel);
        }
        else
        {
            var task = new Models.Task
            {
                Name = addTaskViewModel.Name,
                Description = addTaskViewModel.Description,
                Difficulty = (TaskDiff)Enum.ToObject(typeof(TaskDiff), addTaskViewModel.Difficulty),
                TaskStatus = TaskStatus.Open,
                TimeEstimation = addTaskViewModel.TimeEstimation,
                IsArchive = false,
                TimeSpent = 0,
                OrganizationId = addTaskViewModel.OrganizationId,


            };
                _taskRepository.Add(task, addTaskViewModel.userEmail);
        }
        
        return View("Index");
    }
    [HttpPost]
    public async Task<IActionResult> AddTime(int taskId, string addedTime, string email)
    {
        _taskRepository.AddTime(taskId, double.Parse(addedTime));

        return RedirectToAction("TasksByUser", new { email });
    }
}
    