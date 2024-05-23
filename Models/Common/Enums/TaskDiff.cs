namespace MyTaskTracker.Models.Common.Enums;

public enum TaskDiff
{
    Easy = 0, //0
    Medium = 1, //1
    Hard = 2 //2
}

public static class TaskDiffExtensions
{
    public static string ToFriendlyString(TaskDiff taskDiff)
    {
        switch (taskDiff)
        {
            case TaskDiff.Easy:
                return "Легкая";
            case TaskDiff.Medium:
                return "Средняя";
            case TaskDiff.Hard:
                return "Сложная";
            default:
                throw new ArgumentOutOfRangeException(nameof(taskDiff), taskDiff, null);
        }
    }
}
    
