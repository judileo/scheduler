using scheduler.core.Data;
using scheduler.core.Entities;
using scheduler.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace scheduler.core.Respositories
{
    public sealed class EventRepository : IEventRepository
    {
        private readonly DataContext _context;

        public EventRepository(DataContext context)
        {
            _context = context;
        }


        public List<Event> GetAll()
        {
            var result = _context.Events.ToList();

            return result;
        }

        public void Create(Event entity)
        {
            _context.Events.Add(entity);
            _context.SaveChanges();
        }

        public Event GetById(Guid eventId)
        {
            var result = _context.Events.FirstOrDefault(x => x.Id == eventId);
            return result;
        }

        public void Delete(Event entity)
        {
            _context.Events.Remove(entity);
            _context.SaveChanges();
        }
    }
}
