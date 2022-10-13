using scheduler.core.Entities;
using System.Collections.Generic;

namespace scheduler.core.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAll();
    }
}
