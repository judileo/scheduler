using scheduler.core.Data;
using scheduler.core.Entities;
using scheduler.core.Enums;
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
            ///I choosed put it in repository because now is important data for us, no for the client or user.
            ///I couldn't try this beacuse i can't connect to the database
            foreach (var item in entity.Students)
            {
                Console.WriteLine(item.Email);
            }
        }

        public bool ChangeStatus(Guid id)
        {
            var entity = _context.Events.Where(x => x.Id == id).FirstOrDefault();

            if (entity != null)
            {

                entity.EventStatusId = (int)EventStatusEnum.Cancelled;

                _context.Update(entity);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
