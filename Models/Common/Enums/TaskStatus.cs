namespace MyTaskTracker.Models.Common.Enums;

public enum TaskStatus
{
    Open, //задача находится в начальном состоянии, готова для назначения на исполнителя.
    InProgress, //задача находится в реализации.
    Resolved, //реализована, и эта задача ожидает проверки
    Closed//работы по задаче завершены
}