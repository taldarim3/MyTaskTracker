using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyTaskTracker.Data;
using MyTaskTracker.Interfaces;
using Task = MyTaskTracker.Models.Task;

namespace MyTaskTracker.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskTrackerDbContext _context;

        public TaskRepository(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public bool Add(Task task, string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            var newTask = _context.Add(task);
            _context.SaveChanges();
            
            var sql = $"update \"Tasks\" set \"Email\" = '{userEmail}' where \"Id\" = {newTask.Entity.Id}";

            return Save();
        }

        public bool Update(Task Task)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Task task)
        {
            _context.Remove(task);
            return Save();
        }

        public async Task<IEnumerable<Task>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetAllByUser(string email)
        {
            return await _context.Tasks.FromSqlRaw($"select * from \"Tasks\" where \"Email\" = '{email}'")
                .ToListAsync();
            ;
        }

        public Task<Task?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Task>> GetSliceAsync(int offset, int size)
        {
            return await _context.Tasks.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<Task?> lGetByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Task?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool SetNextStatus(int taskId, bool step)
        {
            var task = _context.Tasks.FirstOrDefault(i => i.Id == taskId);
            if (task != null)
            {
                if (step)
                {
                    task.TaskStatus += 1;
                    _context.SaveChanges();
                }
                else
                {
                    task.TaskStatus -= 1;
                    _context.SaveChanges();
                }
            }

            return Save();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Tasks.CountAsync();
        }

        public async Task<bool> AddTime(int taskId, double addedTime)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return false;
            }

            task.TimeSpent += addedTime;
            _context.SaveChanges();
            return true;
        }
    }
}