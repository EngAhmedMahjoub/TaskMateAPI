using Microsoft.EntityFrameworkCore;
using TaskMateAPI.Models;

namespace TaskMateAPI.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }
}
