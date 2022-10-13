using Microsoft.EntityFrameworkCore;
using scheduler.core.Entities;
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
            var seedData = new List<Rol>()
            {
                new Rol(){ Id = 100, Name= "Estudiante"},
                new Rol(){ Id= 105,Name= "Profe"},
                new Rol(){ Id= 70, Name= "Admin"}
            };

            modelBuilder.Entity<Rol>().HasData(seedData);
        }
    }
}
