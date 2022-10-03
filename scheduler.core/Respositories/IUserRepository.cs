using scheduler.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Respositories
{
    public interface IUserRepository
    {
        public List<User> GetAll();
    }
}
