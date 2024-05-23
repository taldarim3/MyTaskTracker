using Microsoft.EntityFrameworkCore;
using MyTaskTracker.Data;
using MyTaskTracker.Interfaces;
using MyTaskTracker.Models;

namespace MyTaskTracker.Repository
{
    public class OrganizationRepository: IOrganizationRepository
    {
        private readonly TaskTrackerDbContext _context;

        public OrganizationRepository(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public bool Add(Organization organization)
        {
            _context.Add(organization);
            return Save();
        }

        public bool Delete(Organization organization)
        {
            _context.Remove(organization);
            return Save();
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await _context.Organizations.ToListAsync();
        }
        

        public async Task<IEnumerable<Organization>> GetSliceAsync(int offset, int size)
        {
            return await _context.Organizations.Skip(offset).Take(size).ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(int id)
        {
            return await _context.Organizations.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Organization?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Organizations.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Organization organization)
        {
            _context.Update(organization);
            return Save();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Organizations.CountAsync();
        }

    }
}
