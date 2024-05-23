using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTaskTracker.Models;

namespace MyTaskTracker.Data;

public class TaskTrackerDbContext : IdentityDbContext<User>
{
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options) : base(options)
    {

    }
    public DbSet<Models.User> Users { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<Organization> Organizations { get; set; }
}