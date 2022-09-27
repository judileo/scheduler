using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string Begin { get; set; } // fixear 00-24
        public string End { get; set; } // fixear 00-24
        //public string Duration { get => End - Begin; }
        public int FreeSlots { get; set; }
        public int MaxCapacity { get; set; }
        public List<User> Students { get; set; }
        public User Instructor { get; set; }
        public string Description { get; set; }
    }

}
