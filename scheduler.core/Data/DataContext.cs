using Microsoft.EntityFrameworkCore;
using scheduler.core.Entities;
using scheduler.core.Enums;
using System.Collections.Generic;

namespace scheduler.core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }


        public DbSet<Event> Events { get; set; }
        public DbSet<EventStatus> EventStatus { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seedDataRoles = new List<Rol>()
            {
                new Rol(){ Id = 100, Name= nameof(UserRolEnum.Student)},
                new Rol(){ Id= 105,Name= nameof(UserRolEnum.Instructor)},
                new Rol(){ Id= 70, Name= nameof(UserRolEnum.Admin)}
            };

            var seedDataEventStatus = new List<EventStatus>()
            {
                new EventStatus(){ EventStatusId = 200, EventStatusName = nameof(EventStatusEnum.Available)},
                new EventStatus(){ EventStatusId = 400, EventStatusName = nameof(EventStatusEnum.Cancelled)},
                new EventStatus(){ EventStatusId = 500, EventStatusName = nameof(EventStatusEnum.Deleted)},
            };

            modelBuilder.Entity<Rol>().HasData(seedDataRoles);
            modelBuilder.Entity<EventStatus>().HasData(seedDataEventStatus);
        }
    }
}
