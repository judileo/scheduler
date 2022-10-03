using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Dtos.Responses
{
    public class GetEventStatusDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
