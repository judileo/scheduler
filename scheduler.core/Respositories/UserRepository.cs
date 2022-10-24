using Microsoft.EntityFrameworkCore;
using scheduler.core.Data;
using scheduler.core.Entities;
using scheduler.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace scheduler.core.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.Include(x => x.Rol).ToList();
        }
    }
}
