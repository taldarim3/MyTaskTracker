using MyTaskTracker.Models;

namespace MyTaskTracker.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAll();

        Task<IEnumerable<Organization>> GetSliceAsync(int offset, int size);

        Task<Organization?> GetByIdAsync(int id);

        Task<Organization?> GetByIdAsyncNoTracking(int id);

        Task<int> GetCountAsync();

        bool Add(Organization Organization);

        bool Update(Organization Organization);

        bool Delete(Organization Organization);

        bool Save();
    }
}
