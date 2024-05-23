using MyTaskTracker.Models.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = MyTaskTracker.Models.Common.Enums.TaskStatus;

namespace MyTaskTracker.Models;

public class Task
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; } 
    
    public TaskDiff Difficulty { get; set; }
    
    public TaskStatus TaskStatus { get; set; }

    public double TimeEstimation { get; set; }

    [ForeignKey("Email")]
    public virtual User User { get; set; }

    public int OrganizationId { get; set; }

    public virtual Organization Organization { get; set; }
    
    public bool IsArchive { get; set;}
    
    public double TimeSpent { get; set; }
} 