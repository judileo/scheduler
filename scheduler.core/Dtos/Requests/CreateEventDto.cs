using System;

namespace scheduler.core.Dtos.Requests
{
    public class CreateEventDto
    {
        public DayOfWeek Day { get; set; }
        public string Begin { get; set; } // fixear 00-24
        public string End { get; set; } // fixear 00-24
        public int MaxCapacity { get; set; }
        public Guid InstructorId { get; set; }
        public string Description { get; set; }
    }
}
