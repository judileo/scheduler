using Microsoft.EntityFrameworkCore;
using scheduler.core.Entities;

namespace scheduler.core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventStatus> EventStatus { get; set; }
    }
}
