using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Dtos.Requests
{
    public class AddUsersToEventDto
    {
        public List<string> Users { get; set; }
        public Guid EventId { get; set; }
    }
}
