using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Data
{
    public class TaskDataContext : DbContext
    {
        public TaskDataContext(DbContextOptions<TaskDataContext> options)
          : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
