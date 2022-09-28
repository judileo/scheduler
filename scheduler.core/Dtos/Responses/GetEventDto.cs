using scheduler.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Dtos.Responses
{
    public sealed class GetEventDto
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string Begin { get; set; }
        public string End { get; set; }
        public int FreeSlots { get => MaxCapacity - Students.Count; private set => value = MaxCapacity - Students.Count; }
        public int MaxCapacity { get; set; }
        public List<User> Students { get; set; }
        public User Instructor { get; set; }
        public string Description { get; set; }
    }
}
