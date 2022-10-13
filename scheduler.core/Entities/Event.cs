using System;
using System.Collections.Generic;

namespace scheduler.core.Entities
{
    public class Event //: AuditableEntity
    {
        public Event()
        {
            Students = new List<User>();
        }

        public Event(DayOfWeek day,
                     string begin,
                     string end,
                     int maxCapacity,
                     User instructor = null,
                     string description = null)
        {
            Students = new List<User>();
            Day = day;
            Begin = begin;
            End = end;
            MaxCapacity = maxCapacity;
            Instructor = instructor;
            Description = description;
        }

        public Guid Id { get; private set; }
        public DayOfWeek Day { get; private set; }
        public string Begin { get; private set; } // fixear 00-24
        public string End { get; private set; } // fixear 00-24
        //public string Duration { get => End - Begin; }
        public int FreeSlots { get => MaxCapacity - Students.Count; private set => value = MaxCapacity - Students.Count; }
        public int MaxCapacity { get; private set; }
        public string Description { get; private set; }
        public int EventStatusId { get; set; }
        public string InstructorId { get; private set; }


        public virtual List<User> Students { get; private set; }
        public virtual User Instructor { get; private set; }
        //public virtual EventStatus ProductStatus { get; set; }
    }

}
