using Task = MyTaskTracker.Models.Task;

namespace MyTaskTracker.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAll();
        
        Task<IEnumerable<Task>> GetAllByUser(string email);

        Task<Task?> GetByIdAsync(int id);

        Task<Task?> GetByIdAsyncNoTracking(int id);

        Task<int> GetCountAsync();

        bool Add(Task Task, string userEmail);

        bool SetNextStatus(int id, bool step);

        bool Delete(Task Task);

        bool Save();
        
        Task<bool> AddTime(int id, double time);
    }
}