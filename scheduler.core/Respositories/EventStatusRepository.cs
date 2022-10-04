using scheduler.core.Data;
using scheduler.core.Entities;
using scheduler.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Respositories
{
    public class EventStatusRepository : IEventStatusRepository
    {
        private readonly DataContext _context;

        public EventStatusRepository(DataContext context)
        {
            _context = context;
        }

        public virtual List<EventStatus> GetAll()
        {
            var result = _context.EventStatus
                .Take(5)
                .ToList();

            return result;
        }

        //public EventStatus GetById(int id)
        //{
        //    var result = _context.EventStatus.FirstOrDefault(x => x.EventStatusId == id);
        //    return result;
        //}

        public void Create(EventStatus entity)
        {
            _context.EventStatus.Add(entity);
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var entity = _context.EventStatus.Where(x => x.EventStatusId == id).FirstOrDefault();

            if (entity != null)
            {
                _context.EventStatus.Remove(entity);
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
